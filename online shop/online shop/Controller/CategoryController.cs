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
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }
    
    [HttpDelete("delete-category")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteCategoryDto request)
    {
        await _categoryService.DeleteCategoryAsync(request.CategoryId);
        return Ok("Category deleted successfully");
    }
    
    [HttpPatch("update-{categoryId}-category")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string categoryId,[FromForm] UpdateCategoryDto request)
    {
        await _categoryService.UpdateCategoryAsync(categoryId,request);
        return Ok("Updated successfully !");
    }
    
    
    [HttpPost("create-sub-category")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> AddSubCategoryAsync([FromForm] CreateCategoryDto request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }
    
}