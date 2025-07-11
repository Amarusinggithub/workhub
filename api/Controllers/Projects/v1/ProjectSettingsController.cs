using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Projects.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class ProjectSettingsController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
