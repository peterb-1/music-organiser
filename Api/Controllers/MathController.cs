using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    [HttpGet("add")]
    public int Add([FromQuery] int a, [FromQuery] int b)
    {
        return a + b;
    }
}
