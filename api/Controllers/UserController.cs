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
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            await _service.AddUser(user);

            return CreatedAtAction("GetItem", new { user.Id }, user);
        }

        return new JsonResult("Somthing went wrong") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _service.GetUserById(id);
        if (user == null)
        {
            return NotFound();

        }

        return Ok(user);
    }
}
