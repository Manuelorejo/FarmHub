using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CropRecommendationController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRecommendations(string location, string season)
    {
        // Logic to recommend crops based on location and season
        var recommendations = new List<string> { "Corn", "Wheat", "Rice" };
        return Ok(recommendations);
    }
}