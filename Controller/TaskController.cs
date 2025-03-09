using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly FarmHubDbContext _context;
    private object GetTask;

    public TaskController(FarmHubDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateTask([FromBody] Task task)
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        task.FarmerId = farmerId;
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var tasks = _context.Tasks.Where(t => t.FarmerId == farmerId).ToList();
        return Ok(tasks);
    }

    [HttpGet("status/{status}")]
    public IActionResult GetTasksByStatus(string status)
    {
        var farmerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var tasks = _context.Tasks.Where(t => t.FarmerId == farmerId && t.Status == status).ToList();
        return Ok(tasks);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return NoContent();
    }
}