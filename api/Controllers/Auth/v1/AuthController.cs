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
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1")]
public class AuthController(IAuthService service, ILogger<AuthController> logger) : ControllerBase
{
    private readonly IAuthService _service = service ?? throw new ArgumentNullException(nameof(service));
    private readonly ILogger<AuthController> _logger = logger;


    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto) ,200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AuthMe()
    {


        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) )
        {
            return Unauthorized("Invalid or missing user token");
        }

        if (!Guid.TryParse(userIdClaim, out Guid userId))
        {
            return Unauthorized("Invalid user identifier in token");
        }

        UserDto?  response= await _service.GetById(userId);

        if(response==null)  return NotFound("User not found");


        return   Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingUser = await _service.GetByEmail(requestDto.email);
            if (existingUser is not null)
            {
                return BadRequest("Registration failed. Please check your details and try again.");
            }

            var response = await _service.Register(requestDto.lastName, requestDto.firstName, requestDto.password, requestDto.email);
            if (response==null)
            {
                return StatusCode(500, "Registration failed. Please try again later.");
            }


            return Ok(response);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error during user registration for email: {Email}", requestDto.email);
            return StatusCode(500, "Registration failed. Please try again later.");
        }

    }


    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(429)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await  _service.Authenticate(requestDto.email, requestDto.password);

            if (response==null)
            {
                return BadRequest("Invalid credentials. Please check your email and password.");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error during authentication for email: {Email}", requestDto.email);
            return StatusCode(500, "Authentication failed. Please try again later.");
        }


    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Logout( )

    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("Invalid or missing user token");
        }

        if (!Guid.TryParse(userIdClaim, out Guid userId))
        {
            return Unauthorized("Invalid user identifier in token");
        }

        try
        {
            bool response = await _service.Logout(userId);
            if (!response)
            {
                return StatusCode(500, "Logout failed. Please try again.");
            }

            return Ok(new { message = "Successfully logged out", timestamp = DateTime.UtcNow });
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error during logout for user: {UserId}", userId);
            return StatusCode(500, "Logout failed. Please try again.");
        }

    }

    [HttpGet("status")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public IActionResult GetAuthStatus()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

        return Ok(new
        {
            isAuthenticated = true,
            userId = userIdClaim,
            email = emailClaim,
            timestamp = DateTime.UtcNow
        });
    }


}
