using api.Services.Workspaces.interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Workspaces.v1;

[ApiController]
[Route("api/v{version:apiVersion}/workspaces")]
[ApiVersion("1")]
[Authorize]
public class WorkspaceController(IWorkspaceService service) : ControllerBase
{
    private readonly IWorkspaceService _service = service ?? throw new ArgumentNullException(nameof(service));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        bool isCreated = await _service.CreateWorkspace(name);
        if (isCreated) return Ok();
        return BadRequest("Failed to create workspace.");
    }

    [HttpPut("{id}")]
    public IActionResult Edit(Guid id)
    {
        return StatusCode(501, "Edit workspace is not yet implemented.");
    }

    [HttpPost("{id}/members")]
    public IActionResult AddUser(Guid id)
    {
        return StatusCode(501, "Add workspace member is not yet implemented.");
    }
}
