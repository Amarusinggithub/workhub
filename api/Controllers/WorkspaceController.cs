using Microsoft.AspNetCore.Mvc;
using api.Services.interfaces;
using api.Models;
using Asp.Versioning;



namespace api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1")]

public class WorkspaceController(IWorkSpaceService service) : ControllerBase
{


    private readonly IWorkSpaceService _service = service ?? throw new ArgumentNullException(nameof(service));



    [HttpPost]
    [Route("create-workspace")]
    public async Task<IActionResult> Create([FromBody] string name)
    {


        bool IsCreated = await _service.CreateWorkspace(name);

        if (IsCreated) return Ok();

        return BadRequest("");

    }



     [HttpPost]
    [Route("edit-workspace")]
    public async Task<IActionResult> Edit([FromBody] string id)
    {


        bool IsCreated = await _service.CreateWorkspace(id);

        if (IsCreated) return Ok();

return BadRequest("");

    }
}
