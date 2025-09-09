using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DemoController : ControllerBase
{
    [Authorize]
    [HttpGet("protegido")]
    public IActionResult Protegido()
    {
        return Ok(new { mensaje = "Token Jwt Valido" });
    }
}
