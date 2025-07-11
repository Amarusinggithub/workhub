using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Analytics.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class AnalyticsController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
