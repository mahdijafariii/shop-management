using online_shop.DTO;

namespace online_shop.Services.CategoryService;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
}