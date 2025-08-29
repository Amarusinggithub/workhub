using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using api.Services.Infanstructure.interfaces;
using api.Services.interfaces;

namespace api.Services.Infanstructure;

public class StorageService : IStorageService
{
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _region;
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public StorageService(IConfiguration config)
    {
        // Use AWS SDK's default credential chain to pick up credentials from ~/.aws/credentials
        _s3Client = new AmazonS3Client(); // Automatically picks credentials from ~/.aws/credentials
        _bucketName = config["AWS:BucketName"]; // The retrieve bucket name appsettings.json
    }

    public AmazonS3Client CreateS3Client()
    {
        var credentials = new BasicAWSCredentials(_accessKey, _secretKey);
        var region = RegionEndpoint.GetBySystemName(_region);
        return new AmazonS3Client(credentials, region);
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var key = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        using var stream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        var response = await _s3Client.PutObjectAsync(putRequest);

        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            return key; // Return the key to access the file.
        }

        throw new Exception("File upload failed.");
    }

    public async Task SetVersioningAsync(bool enableVersioning)
    {
        var versioningConfig = new S3BucketVersioningConfig
        {
            Status = enableVersioning ? VersionStatus.Enabled : VersionStatus.Suspended
        };

        var request = new PutBucketVersioningRequest
        {
            BucketName = _bucketName,
            VersioningConfig = versioningConfig
        };

        try
        {
            await _s3Client.PutBucketVersioningAsync(request);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while setting versioning: {ex.Message}");
        }
    }

    public async Task<Stream> GetFileStreamAsync(string key, string versionId = null)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            VersionId = versionId
        };
        try
        {
            var response = await _s3Client.GetObjectAsync(getRequest);
            return response.ResponseStream; // Return the stream directly.
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving the file: {ex.Message}");
        }
    }

    public async Task<string> UpdateFileAsync(string key, IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,  // Use the existing key to overwrite the file.
            InputStream = stream,
            ContentType = file.ContentType
        };

        var response = await _s3Client.PutObjectAsync(putRequest);
        // With versioning enabled, this response contains a VersionId

        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            return response.VersionId;  // Return the version ID for tracking the updated file version.
        }

        throw new Exception("File update failed.");
    }
    public async Task<bool> DeleteFileAsync(string key, string versionId = null)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            VersionId = versionId  // If versionId is provided, it deletes that specific version.
        };

        try
        {
            var response = await _s3Client.DeleteObjectAsync(deleteRequest);
            return response.HttpStatusCode == HttpStatusCode.NoContent;
        }
        catch (AmazonS3Exception ex)
        {
            throw new Exception($"Error encountered while deleting file: {ex.Message}");
        }
    }

    public string GeneratePresignedUrl(string key, int expirationInMinutes)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
            Verb = HttpVerb.GET // This specifies read-only access. Use HttpVerb.PUT for generating upload URLs
        };

        try
        {
            var url = _s3Client.GetPreSignedURL(request);
            return url; // Returns the presigned URL
        }
        catch (AmazonS3Exception ex)
        {
            throw new Exception($"Error generating presigned URL: {ex.Message}");
        }
    }


    public async Task<string> ParallelMultipartUploadAsync(IFormFile file)
{
    var uploadId = await InitiateMultipartUploadAsync(file.FileName);
    var partETags = new List<PartETag>();

    try
    {
        using var stream = file.OpenReadStream();
        const int partSize = 5 * 1024 * 1024; // 5 MB
        var buffer = new byte[partSize];
        int bytesRead;
        int partNumber = 1;

        var uploadTasks = new List<Task<UploadPartResponse>>();

        while ((bytesRead = await stream.ReadAsync(buffer, 0, partSize)) > 0)
        {
            var memoryStream = new MemoryStream(buffer, 0, bytesRead);

            var uploadPartRequest = new UploadPartRequest
            {
                BucketName = _bucketName,
                Key = file.FileName,
                UploadId = uploadId,
                PartNumber = partNumber,
                InputStream = memoryStream,
                PartSize = bytesRead
            };

            // Create a task to upload the part
            var uploadTask = _s3Client.UploadPartAsync(uploadPartRequest);
            uploadTasks.Add(uploadTask);

            partNumber++;
        }

        // Wait for all the upload tasks to complete
        var uploadResponses = await Task.WhenAll(uploadTasks);

        // Collect the ETags for completing the multipart upload
        partETags = uploadResponses
            .Select((response, index) => new PartETag(index + 1, response.ETag))
            .ToList();

        return await CompleteMultipartUploadAsync(file.FileName, uploadId, partETags);
    }
    catch (Exception ex)
    {
        await AbortMultipartUploadAsync(file.FileName, uploadId);
        throw new Exception($"Parallel multipart upload failed: {ex.Message}");
    }
}
private async Task<string> InitiateMultipartUploadAsync(string key)
{
    var initiateRequest = new InitiateMultipartUploadRequest
    {
        BucketName = _bucketName,
        Key = key
    };

    var initiateResponse = await _s3Client.InitiateMultipartUploadAsync(initiateRequest);
    return initiateResponse.UploadId;
}

private async Task<string> CompleteMultipartUploadAsync(string key, string uploadId, List<PartETag> partETags)
{
    var completeRequest = new CompleteMultipartUploadRequest
    {
        BucketName = _bucketName,
        Key = key,
        UploadId = uploadId,
        PartETags = partETags
    };

    var completeResponse = await _s3Client.CompleteMultipartUploadAsync(completeRequest);
    return completeResponse.Location; // Returns the URL of the uploaded object
}

private async Task AbortMultipartUploadAsync(string key, string uploadId)
{
    var abortRequest = new AbortMultipartUploadRequest
    {
        BucketName = _bucketName,
        Key = key,
        UploadId = uploadId
    };

    await _s3Client.AbortMultipartUploadAsync(abortRequest);
}

}

