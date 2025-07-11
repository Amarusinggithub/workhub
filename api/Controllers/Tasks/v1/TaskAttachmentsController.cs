using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Tasks.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class TaskAttachmentsController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
