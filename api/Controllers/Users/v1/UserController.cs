using api.DTOs.Users;
using api.Services.Users.interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Users.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service ?? throw new ArgumentNullException(nameof(service));


    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithRolesDto>> GetUserWithRoles(int id)
    {
        var user = await _service.GetById(id);

        if (user == null)
            return NotFound();

        //var userDto = _mapper.Map<UserWithRolesDto>(user);
        //userDto.Roles = (await _service.GetRolesAsync(user)).ToList();

        return Ok();
    }


}


