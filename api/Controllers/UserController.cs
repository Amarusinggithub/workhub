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


    [HttpPost]
[Route("register")]
    public async Task<IActionResult> Register([FromBody]string firstName,[FromBody] string lastName,[FromBody]string email,[FromBody] string password)
    {

  User? user = await _service.GetByEmail(email);

        if (user is null)
        {
            throw new Exception("User was not found");
        }

        if (user.Email== email)
        {
            return new JsonResult("user with this email already exist") { StatusCode = 400 };
        }

        var newUser=await _service.AddUser(firstName,lastName,email,password);

        if(newUser==false) return new JsonResult("Somthing went wrong") { StatusCode = 500 };

        return Ok();
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] string email,[FromBody] string password)
    {

        User? user = await _service.GetByEmail(email);

        if (user is null)
        {
            return new JsonResult("no user with this email exist") { StatusCode = 400 };
        }

        var isVerified = user.PasswordHash != null &&  _service.Authenticate(password,user.PasswordHash);

        if(isVerified==false) return new JsonResult("Incorrect Password") { StatusCode = 400 };
        return Ok(user);
    }
}
