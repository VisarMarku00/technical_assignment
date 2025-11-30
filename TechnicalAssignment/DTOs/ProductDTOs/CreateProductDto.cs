using System.ComponentModel.DataAnnotations;
public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Category { get; set; }
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
    public int StockQuantity { get; set; }
}