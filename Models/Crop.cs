public class Crop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime HarvestDate { get; set; }
    public int FarmerId { get; set; }
    public Farmer Farmer { get; set; }
}