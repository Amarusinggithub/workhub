using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Workspaces.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class WorkspaceInvitesController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
