using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    [HttpPost("add")]
    public IActionResult Add(MathRequest request)
    {
        return Ok(new MathResult { Result = request.A + request.B });
    }
}
