using System.Security.Claims;
using api.DTOs.Auth.Requests;
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
public class AuthController(IAuthService service,IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    private readonly IAuthService _service = service ?? throw new ArgumentNullException(nameof(service));
    private readonly IHttpContextAccessor _httpContextAccessor=httpContextAccessor;


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {

       // _httpContextAccessor!.HttpContext.GetTokenAsync();

        return Ok();
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
    public IActionResult Logout()
    {
        Response.Cookies.Delete("ACCESS_TOKEN");
        return Ok();
    }


}
