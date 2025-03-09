public class Storage
{
    public int Id { get; set; }
    public string CropName { get; set; }
    public int Quantity { get; set; }
    public int FarmerId { get; set; }
    public Farmer Farmer { get; set; }
}