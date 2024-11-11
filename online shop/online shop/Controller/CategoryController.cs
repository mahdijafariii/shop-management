using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Services.CategoryService;

namespace online_shop.Controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("create-category")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> AddCategoryAsync([FromForm] CreateCategoryDto request)
    {
        Console.WriteLine(request.Filters.Count);
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }
    
}