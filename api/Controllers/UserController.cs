using api.DTOs;
using api.Models;
using api.Services.interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1")]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service ?? throw new ArgumentNullException(nameof(service));


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _service.GetByEmail(request.email);
        if (existingUser is not null)
        {
            return BadRequest("User with this email already exists");
        }

        var success = await _service.AddUser(request.lastName, request.firstName, request.email, request.password);
        if (!success)
        {
            return StatusCode(500, "Something went wrong while creating the user");
        }

        return StatusCode(201, "Account created");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _service.GetByEmail(request.Email);
        if (user is null)
        {
            return BadRequest("No user with this email exists");
        }

        var isVerified = user.PasswordHash != null && _service.Authenticate(request.Password, user.PasswordHash);

        if (!isVerified)
        {
            return BadRequest("Incorrect password");
        }

        var response = new UserDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            ProfilePicture = user.ProfilePicture,
            IsActive = user.IsActive,
            HeaderImage = user.HeaderImage,
            JobTItle = user.JobTItle,
            Organization = user.Organization,
            Location = user.Location
        };

        return Ok(response);
    }

}
