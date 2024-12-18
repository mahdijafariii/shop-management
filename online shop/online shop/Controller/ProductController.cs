using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services.ProductService;

namespace online_shop.Controller;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProductAsync([FromForm] CreateProductDto request)
    {
        await _productService.CreateProduct(request);
        return Ok("added successfully");
    }
    
    
    [HttpPost("get-product")]
    public async Task<IActionResult> GetProductAsync([FromQuery] string productId)
    {
        var product = await _productService.GetProduct(productId);
        return Ok(product);
    }
    
    [HttpDelete("delete-product")]
    public async Task<IActionResult> DeleteProductAsync([FromQuery] string request)
    {
        await _productService.DeletesProduct(request);
        return Ok("deleted successfully");
    }
}