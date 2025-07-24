using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.System.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class IntegrationsController : ControllerBase
{

}
