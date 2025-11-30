public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
}