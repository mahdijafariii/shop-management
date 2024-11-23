using MongoDB.Bson;

namespace online_shop.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(string categoryId);
}