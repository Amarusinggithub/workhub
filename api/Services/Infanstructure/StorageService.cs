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
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public StorageService(IConfiguration config)
    {
        var accessKey = config["AWS:AccessKey"]
            ?? throw new InvalidOperationException("AWS:AccessKey is not configured");
        var secretKey = config["AWS:SecretKey"]
            ?? throw new InvalidOperationException("AWS:SecretKey is not configured");
        var region = config["AWS:Region"] ?? "us-east-1";
        var serviceUrl = config["AWS:ServiceUrl"];
        _bucketName = config["AWS:BucketName"] ?? "workhub";

        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        var s3Config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(region),
            ForcePathStyle = true,
        };

        if (!string.IsNullOrEmpty(serviceUrl))
        {
            s3Config.ServiceURL = serviceUrl;
        }

        _s3Client = new AmazonS3Client(credentials, s3Config);
    }

    public AmazonS3Client CreateS3Client()
    {
        return (AmazonS3Client)_s3Client;
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
            return key;
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

        await _s3Client.PutBucketVersioningAsync(request);
    }

    public async Task<Stream> GetFileStreamAsync(string key, string versionId = null)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            VersionId = versionId
        };

        var response = await _s3Client.GetObjectAsync(getRequest);
        return response.ResponseStream;
    }

    public async Task<string> UpdateFileAsync(string key, IFormFile file)
    {
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
            return response.VersionId;
        }

        throw new Exception("File update failed.");
    }

    public async Task<bool> DeleteFileAsync(string key, string versionId = null)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            VersionId = versionId
        };

        var response = await _s3Client.DeleteObjectAsync(deleteRequest);
        return response.HttpStatusCode == HttpStatusCode.NoContent;
    }

    public string GeneratePresignedUrl(string key, int expirationInMinutes)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
            Verb = HttpVerb.GET
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public async Task<string> ParallelMultipartUploadAsync(IFormFile file)
    {
        var uploadId = await InitiateMultipartUploadAsync(file.FileName);
        var partETags = new List<PartETag>();

        try
        {
            using var stream = file.OpenReadStream();
            const int partSize = 5 * 1024 * 1024;
            var buffer = new byte[partSize];
            int partNumber = 1;
            var uploadTasks = new List<Task<UploadPartResponse>>();

            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, partSize)) > 0)
            {
                var memoryStream = new MemoryStream(buffer, 0, bytesRead);
                uploadTasks.Add(_s3Client.UploadPartAsync(new UploadPartRequest
                {
                    BucketName = _bucketName,
                    Key = file.FileName,
                    UploadId = uploadId,
                    PartNumber = partNumber++,
                    InputStream = memoryStream,
                    PartSize = bytesRead
                }));
            }

            var uploadResponses = await Task.WhenAll(uploadTasks);
            partETags = uploadResponses
                .Select((r, i) => new PartETag(i + 1, r.ETag))
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
        var response = await _s3Client.InitiateMultipartUploadAsync(new InitiateMultipartUploadRequest
        {
            BucketName = _bucketName,
            Key = key
        });
        return response.UploadId;
    }

    private async Task<string> CompleteMultipartUploadAsync(string key, string uploadId, List<PartETag> partETags)
    {
        var response = await _s3Client.CompleteMultipartUploadAsync(new CompleteMultipartUploadRequest
        {
            BucketName = _bucketName,
            Key = key,
            UploadId = uploadId,
            PartETags = partETags
        });
        return response.Location;
    }

    private async Task AbortMultipartUploadAsync(string key, string uploadId)
    {
        await _s3Client.AbortMultipartUploadAsync(new AbortMultipartUploadRequest
        {
            BucketName = _bucketName,
            Key = key,
            UploadId = uploadId
        });
    }
}
