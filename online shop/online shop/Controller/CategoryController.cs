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
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> AddCategoryAsync([FromBody] CreateCategoryDto request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }
    
}