using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CropController : ControllerBase
{
    private readonly FarmHubDbContext _context;
    private object GetCrop;

    public CropController(FarmHubDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddCrop([FromBody] Crop crop)
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        crop.FarmerId = farmerId;
        _context.Crops.Add(crop);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, crop);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCrop(int id)
    {
        var crop = _context.Crops.Find(id);
        if (crop == null) return NotFound();
        _context.Crops.Remove(crop);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPost("move-to-storage")]
    public IActionResult MoveToStorage([FromBody] Storage storage)
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        storage.FarmerId = farmerId;
        _context.Storage.Add(storage);
        _context.SaveChanges();
        return Ok("Crop moved to storage.");
    }

    [HttpGet("storage")]
    public IActionResult GetStorage()
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var storage = _context.Storage.Where(s => s.FarmerId == farmerId).ToList();
        return Ok(storage);
    }
}