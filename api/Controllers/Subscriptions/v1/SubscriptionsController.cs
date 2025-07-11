using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Subscriptions.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1")]
public class SubscriptionsController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}
