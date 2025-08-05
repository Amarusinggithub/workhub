using System.Security.Claims;
using api.DTOs.Auth.Requests;
using api.DTOs.Users;
using api.Services.Auth.interfaces;
using api.Services.Users.interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Auth.v1;

[ApiController]
[Route("api/auth")]
[ApiVersion("1")]
public class AuthController(IAuthService service) : ControllerBase
{
    private readonly IAuthService _service = service ?? throw new ArgumentNullException(nameof(service));


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> AuthMe()
    {


        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            return Unauthorized("Invalid token");
        }

        UserDto?  response= await _service.GetById(userId);

        if(response==null)  return NotFound("User not found");


        return   Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _service.GetByEmail(requestDto.email);
        if (existingUser is not null)
        {
            return BadRequest("User with this email already exists");
        }

        var response = await _service.Register(requestDto.lastName, requestDto.firstName, requestDto.password, requestDto.email);
        if (response==null)
        {
            return StatusCode(500, "Something went wrong while creating the user");
        }


        return Ok(response);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await  _service.Authenticate(requestDto.email, requestDto.password);

        if (response==null)
        {
            return BadRequest("Invalid email or password");
        }

        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout( )

    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            return Unauthorized("Invalid token");
        }

        bool response = await _service.Logout(userId);
        if (!response)
        {
            return StatusCode(500, "Logout failed");
        }

        return Ok(new { message = "Logged out successfully" });
    }


}
