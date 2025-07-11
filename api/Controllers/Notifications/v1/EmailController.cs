using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Notifications.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class EmailController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
