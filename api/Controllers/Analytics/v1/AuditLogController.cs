using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Analytics.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class AuditLogController : ControllerBase
{
    // GET
    [HttpGet("auditlogs")]

    public IActionResult Index()
    {
        return Ok();
    }
}
