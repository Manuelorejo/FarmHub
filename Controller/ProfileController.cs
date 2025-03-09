using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly FarmHubDbContext _context;

    public ProfileController(FarmHubDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetProfileLink()
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var farmer = _context.Farmers.Find(farmerId);
        var link = $"https://farmhub.com/profile/{farmerId}";
        return Ok(new { Link = link });
    }
}