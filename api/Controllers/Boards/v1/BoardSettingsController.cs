using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Boards.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class BoardSettingsController : ControllerBase
{
    // GET
    [HttpGet("board-settings")]

    public IActionResult Index()
    {
        return Ok();
    }
}
