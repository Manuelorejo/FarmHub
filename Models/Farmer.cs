public class Farmer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string State { get; set; }
    public List<Task> Tasks { get; set; } = new();
    public List<Crop> Crops { get; set; } = new();
    public List<Storage> Storage { get; set; } = new();
}