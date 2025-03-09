using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly FarmHubDbContext _context;
    private readonly IConfiguration _configuration;

    public object BCrypt { get; private set; }

    public AuthController(FarmHubDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("signup")]
    public IActionResult Signup([FromBody] Farmer farmer)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(farmer.PasswordHash);
        farmer.PasswordHash = hashedPassword;
        _context.Farmers.Add(farmer);
        _context.SaveChanges();
        return Ok("Farmer registered successfully.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var farmer = _context.Farmers.FirstOrDefault(x => x.Email == model.Email);
        if (farmer != null && BCrypt.Net.BCrypt.Verify(model.Password, farmer.PasswordHash))
        {
            var token = GenerateToken(farmer);
            return Ok(new { Token = token });
        }
        return Unauthorized("Invalid credentials.");
    }

    private string GenerateToken(Farmer farmer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, farmer.Id.ToString()),
            new Claim(ClaimTypes.Email, farmer.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}