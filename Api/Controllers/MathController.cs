using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    [HttpPost("add")]
    public MathResult Add(MathRequest request)
    {
        return new MathResult { Result = request.A + request.B };
    }
}
