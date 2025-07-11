using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Auth.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class EmailVerificationController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
