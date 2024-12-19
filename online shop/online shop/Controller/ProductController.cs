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
    [HttpGet("get-product-shortIdentifier")]
    public async Task<IActionResult> GetProductShortIdentifierAsync([FromQuery] string shortIdentifier)
    {
        var product = await _productService.GetProductWithIdentifier(shortIdentifier);
        return Ok(product);
    }
    
    [HttpDelete("delete-product")]
    public async Task<IActionResult> DeleteProductAsync([FromQuery] string request)
    {
        await _productService.DeletesProduct(request);
        return Ok("deleted successfully");
    }
    
    [HttpPatch("update-product")]
    public async Task<IActionResult> UpdateProductAsync([FromForm]UpdateProduct request)
    {
        await _productService.UpdateProduct(request);
        return Ok("updated successfully");
    }
}