using MongoDB.Bson;
using online_shop.DTO;

namespace online_shop.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(string categoryId);
    Task<bool> UpdateCategoryAsync(string categoryId, UpdateCategoryDto updateCategoryDto);
}