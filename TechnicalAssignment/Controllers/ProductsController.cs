using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly ProductsService productsService;

    public ProductsController(ProductsService productsService)
    {
        this.productsService = productsService;
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetProductById(int id)
    {
        var product = productsService.GetProductById(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }
        return Ok(product);
    }
    [HttpGet]
    public ActionResult<List<ProductDto>> GetProducts(
        string? category = null,
        int? minPrice = null,
        int? maxPrice = null,
        string? sortBy = null,
        string? sortOrder = "asc",
        int pageNumber = 1,
        int pageSize = 10)
    {
        var products = productsService.GetProducts(category, minPrice, maxPrice, sortBy, sortOrder, pageNumber, pageSize);
        return Ok(products);
    }
    [HttpPost]
    public ActionResult<ProductDto> CreateProduct([FromBody] CreateProductDto product)
    {
        var newProduct = productsService.CreateProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { Id = newProduct.Id }, newProduct);
    }
    [HttpPut("{id}")]
    public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] UpdateProductDto product)
    {
        var updatedProduct = productsService.UpdateProduct(id, product);
        if (updatedProduct == null)
        {
            return NotFound(new { message = "Product not found" });
        }
        return Ok(updatedProduct);
    }
    [HttpDelete("{id}")]
    public ActionResult<ProductDto> DeleteProduct(int id)
    {
        var product = productsService.DeleteProduct(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }
        return Ok(product);
    }
}