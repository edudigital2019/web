using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config) => _config = config;

    public record LoginDto(string User, string Password);

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {

        if (dto.User == "Uniandes" && dto.Password == "1234")
        {
            var token = GenerarToken(dto.User);
            return Ok(new { token });
        }
        return Unauthorized("Usuario o clave incorrecta");
    }

    private string GenerarToken(string user)
    {
        var jwt = _config.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user),
            new Claim("role", "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
