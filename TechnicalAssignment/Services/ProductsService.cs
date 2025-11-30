public class ProductsService
{

    private readonly AppDbContext dbContext;

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CreatedAt = product.CreatedAt
        };
    }

    public ProductsService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public ProductDto? GetProductById(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
        var productDto = new ProductDto();

        return MapToDto(product);
    }
    public List<ProductDto> GetProducts(string? category = null, int? minPrice = null, int? maxPrice = null)
    {
        var products = dbContext.Products.AsQueryable();
        var productDto = new ProductDto();
        var productsDto = new List<ProductDto>();

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category == category);
        }
        if (minPrice.HasValue)
        {
            products = products.Where(p => p.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= maxPrice.Value);
        }

        foreach (Product product in products)
        {
            productsDto.Add(MapToDto(product));
        }

        return productsDto;
    }

    public ProductDto CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product();
        var productDto = new ProductDto();

        product.Name = createProductDto.Name;
        product.Category = createProductDto.Category;
        product.Price = createProductDto.Price;
        product.StockQuantity = createProductDto.StockQuantity;
        product.CreatedAt = DateTime.UtcNow;

        dbContext.Products.Add(product);
        dbContext.SaveChanges();

        return MapToDto(product);
    }

    public ProductDto UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);

        product.Name = updateProductDto.Name;
        product.Category = updateProductDto.Category;
        product.Price = updateProductDto.Price;
        product.StockQuantity = updateProductDto.StockQuantity;

        dbContext.SaveChanges();

        return MapToDto(product);
    }

    public ProductDto DeleteProduct(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
        var productDto = MapToDto(product);

        dbContext.Remove(product);
        dbContext.SaveChanges();

        return productDto;
    }
}