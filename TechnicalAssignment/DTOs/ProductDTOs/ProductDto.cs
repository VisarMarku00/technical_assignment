using System.ComponentModel.DataAnnotations;
public class ProductDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Category { get; set; }
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool InStock => StockQuantity > 0;
}