using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Users.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class UserActivityController : ControllerBase
{

}
