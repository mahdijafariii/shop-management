using online_shop.DTO;

namespace online_shop.Services.CategoryService;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<SubCategory> CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto);
    Task UpdateCategoryAsync(string categoryId,UpdateCategoryDto createCategoryDto);

    Task DeleteCategoryAsync(string objectId);
    
    Task<List<SubCategory>> GetALlSubCategories();
    
    Task<List<SubCategory>> GetSubCategories(string categoryId);
    Task DeleteSubCategories(string categoryId);
    Task UpdateSubCategoryAsync(string subCategoryId, UpdateSubCategoryDto updateSubCategoryDto);




}