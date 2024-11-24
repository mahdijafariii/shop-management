using MongoDB.Bson;
using online_shop.DTO;

namespace online_shop.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<SubCategory> CreateSubCategoryAsync(SubCategory subCategory);

    Task<bool> DeleteCategoryAsync(string categoryId);
    Task<bool> UpdateCategoryAsync(string categoryId, UpdateCategoryDto updateCategoryDto);
    Task<bool> CheckParentIdValidator(string parentId);
    
    Task<List<SubCategory>> GetALlSubCategories();
    
    Task<List<SubCategory>> GetSubCategories(string categoryId);
    Task<bool> DeleteSubCategories(string categoryId);

}