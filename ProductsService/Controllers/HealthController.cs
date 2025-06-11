using Microsoft.AspNetCore.Mvc;

namespace ProductsService.Controllers;
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("All good!");
}