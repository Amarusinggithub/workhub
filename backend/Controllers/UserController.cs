using backend.Models;
using backend.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new { user.Id }, user);
        }

        return new JsonResult("Somthing went wrong") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if (user == null)
        {
            return NotFound();

        }

        return Ok(user);
    }
}
