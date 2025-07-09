using System.Web;
using api.Services.Infanstructure;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Softwares.v1;

[ApiController]
[Route("api/[controller]")]
public class StorageController(StorageService storageService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var fileKey = await storageService.UploadFileAsync(file);
            return Ok(new { FileKey = fileKey });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("download/{key}")]
    public async Task<IActionResult> DownloadFile(string key, string fileName)
    {
        try
        {
            var responseStream = await storageService.GetFileStreamAsync(key);
            return File(responseStream, "application/octet-stream", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update/{key}")]
    public async Task<IActionResult> UpdateFile(string key, IFormFile newFile)
    {
        if (newFile == null || newFile.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var updatedFileKey = await storageService.UpdateFileAsync(key, newFile);
            return Ok(new { FileKey = HttpUtility.UrlDecode(updatedFileKey) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete/{key}")]
    public async Task<IActionResult> DeleteFile(string key, [FromQuery] string versionId = null)
    {
        try
        {
            var result = await storageService.DeleteFileAsync(key, versionId);
            return result ? Ok("File deleted successfully.") : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("presigned-url/{key}")]
    public IActionResult GetPresignedUrl(string key, [FromQuery] int expirationInMinutes = 60)
    {
        try
        {
            var url = storageService.GeneratePresignedUrl(key, expirationInMinutes);
            return Ok(new { PresignedUrl = url, Expiration = expirationInMinutes });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("multipart-upload")]
    public async Task<IActionResult> MultipartUpload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var fileUrl = await storageService.ParallelMultipartUploadAsync(file);
            return Ok(new { FileUrl = fileUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

