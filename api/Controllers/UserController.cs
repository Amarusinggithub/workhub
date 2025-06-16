using api.Models;
using api.Services.interfaces;
using api.Repository.interfaces;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController( IUserService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }


    [HttpPost]
[Route("Register")]
    public async Task<IActionResult> Register([FromBody]User user)
    {
        if (ModelState.IsValid)
        {
            await _service.AddUser(user);

            return CreatedAtAction("GetItem", new { user.Id }, user);
        }

        return new JsonResult("Somthing went wrong") { StatusCode = 500 };
    }



    [HttpGet("{id}")]
    [Route("Login")]
    public async Task<IActionResult> Login(int id)
    {
        var user = await _service.GetUserById(id);
        if (user == null)
        {
            return NotFound();

        }

        return Ok(user);
    }
}
