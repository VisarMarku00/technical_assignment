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

        if (product == null)
        {
            return null;
        }

        var productDto = new ProductDto();

        return MapToDto(product);
    }
    public List<ProductDto> GetProducts(string? category = null,
        int? minPrice = null,
        int? maxPrice = null,
        string? sortBy = null,
        string? sortOrder = "asc",
        int pageNumber = 1,
        int pageSize = 10)
    {
        var products = dbContext.Products.AsQueryable();
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

        if (!string.IsNullOrEmpty(sortBy))
        {
            var order = sortOrder?.ToLower() ?? "asc";
            switch (sortBy.ToLower())
            {
                case "name":
                    products = order == "desc" ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                    break;
                case "price":
                    products = order == "desc" ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                    break;
                case "createdat":
                    products = order == "desc" ? products.OrderByDescending(p => p.CreatedAt) : products.OrderBy(p => p.CreatedAt);
                    break;
                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }
        }
        else
        {
            products = products.OrderBy(p => p.Id);
        }

        var skip = (pageNumber - 1) * pageSize;
        products = products.Skip(skip).Take(pageSize);

        foreach (Product product in products!)
        {
            productsDto.Add(MapToDto(product));
        }

        return productsDto;
    }

    public ProductDto CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product();

        product.Name = createProductDto.Name;
        product.Category = createProductDto.Category;
        product.Price = createProductDto.Price;
        product.StockQuantity = createProductDto.StockQuantity;
        product.CreatedAt = DateTime.UtcNow;

        dbContext.Products.Add(product);
        dbContext.SaveChanges();

        return MapToDto(product);
    }

    public ProductDto? UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return null;
        }

        product.Name = updateProductDto.Name;
        product.Category = updateProductDto.Category;
        product.Price = updateProductDto.Price;
        product.StockQuantity = updateProductDto.StockQuantity;

        dbContext.SaveChanges();

        return MapToDto(product);
    }

    public ProductDto? DeleteProduct(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return null;
        }

        var productDto = MapToDto(product);
        dbContext.Remove(product);
        dbContext.SaveChanges();

        return productDto;
    }
}