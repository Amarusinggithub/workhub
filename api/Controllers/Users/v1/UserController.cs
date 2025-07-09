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




}
