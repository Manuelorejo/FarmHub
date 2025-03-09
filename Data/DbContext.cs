using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class FarmHubDbContext : IdentityDbContext<Farmer> // Changed from DbContext to IdentityDbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Crop> Crops { get; set; }
    public DbSet<Storage> Storage { get; set; }

    public FarmHubDbContext(DbContextOptions<FarmHubDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure your models here (if needed)
    }
}