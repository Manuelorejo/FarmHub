public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } // Completed, Ongoing, Overdue
    public int FarmerId { get; set; }
    public Farmer Farmer { get; set; }
}