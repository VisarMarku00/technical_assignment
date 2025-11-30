using System.ComponentModel.DataAnnotations;
public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Category { get; set; } = string.Empty;
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
    public int StockQuantity { get; set; }
}