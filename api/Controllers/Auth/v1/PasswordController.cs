using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Auth.v1;


[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]

public class PasswordController : ControllerBase
{
    // GET
    [HttpGet("reset-password")]

    public IActionResult Index()
    {
        return Ok();
    }
}
