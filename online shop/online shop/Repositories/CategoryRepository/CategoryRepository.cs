using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;

namespace online_shop.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly MongoDbContext _dbContext;

    public CategoryRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        await _dbContext.Categories.InsertOneAsync(category);
        return category;
    }

    public async Task<SubCategory> CreateSubCategoryAsync(SubCategory subCategory)
    {
        await _dbContext.SubCategories.InsertOneAsync(subCategory);
        return subCategory;
    }

    public async Task<bool> UpdateSubCategoryAsync(string subCategoryId, UpdateSubCategoryDto updateSubCategoryDto)
    {
        var filter = Builders<SubCategory>.Filter.Eq(c => c.Id, subCategoryId);

        var update = Builders<SubCategory>.Update;
        var updateDefinition = new List<UpdateDefinition<SubCategory>>();

        if (!string.IsNullOrEmpty(updateSubCategoryDto.Title))
            updateDefinition.Add(update.Set(c => c.Title, updateSubCategoryDto.Title));

        if (!string.IsNullOrEmpty(updateSubCategoryDto.Slug))
            updateDefinition.Add(update.Set(c => c.Slug, updateSubCategoryDto.Slug));

        if (!string.IsNullOrEmpty(updateSubCategoryDto.Parent))
            updateDefinition.Add(update.Set(c => c.ParentId, updateSubCategoryDto.Parent));

        if (!string.IsNullOrEmpty(updateSubCategoryDto.Description))
            updateDefinition.Add(update.Set(c => c.Description, updateSubCategoryDto.Description));

        if (updateSubCategoryDto.Filters != null && updateSubCategoryDto.Filters.Any())
            updateDefinition.Add(update.Set(c => c.Filters, updateSubCategoryDto.Filters));

        if (updateDefinition.Any())
        {
            var combinedUpdate = update.Combine(updateDefinition);
            var result = await _dbContext.SubCategories.UpdateOneAsync(filter, combinedUpdate);

            return result.ModifiedCount > 0;
        }

        return false;
    }

    public async Task<bool> CheckParentIdValidator(string parentId)
    {
        var filter = Builders<Category>.Filter.Eq(u => u.Id, parentId);
        var result = await _dbContext.Categories.FindAsync(filter);
        return result.Any();
    }

    public async Task<List<SubCategory>> GetALlSubCategories()
    {
        var result = await _dbContext.SubCategories.FindAsync(FilterDefinition<SubCategory>.Empty);
        return result.ToList();
    }

    public async Task<List<SubCategory>> GetSubCategories(string categoryId)
    {
        var filter = Builders<SubCategory>.Filter.Eq(u => u.ParentId , categoryId);
        var result = await _dbContext.SubCategories.FindAsync(filter);
        return result.ToList();
    }

    public async Task<List<Category>> GetCategoriesSub(ObjectId? categoryId)
    {
        if (categoryId == null) 
        {
            var mainCategories = await _dbContext.Categories.Find(u => u.ParentId.Equals(null)).ToListAsync();
            return mainCategories;
        }
        var filter = Builders<Category>.Filter.Eq(u => u.ParentId , categoryId);
        var result = await _dbContext.Categories.FindAsync(filter);
        return result.ToList();
    }

    public async Task<bool> DeleteSubCategories(string categoryId)
    {
        var filter = Builders<SubCategory>.Filter.Eq(c => c.Id, categoryId);
        var result = await _dbContext.SubCategories.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }


    public async Task<bool> DeleteCategoryAsync(string categoryId)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
        var result = await _dbContext.Categories.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<bool> UpdateCategoryAsync(string categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);

        var update = Builders<Category>.Update;
        var updateDefinition = new List<UpdateDefinition<Category>>();

        if (!string.IsNullOrEmpty(updateCategoryDto.Title))
            updateDefinition.Add(update.Set(c => c.Title, updateCategoryDto.Title));

        if (!string.IsNullOrEmpty(updateCategoryDto.Slug))
            updateDefinition.Add(update.Set(c => c.Slug, updateCategoryDto.Slug));

        if (updateCategoryDto.Parent.HasValue)
            updateDefinition.Add(update.Set(c => c.ParentId, updateCategoryDto.Parent));

        if (!string.IsNullOrEmpty(updateCategoryDto.Description))
            updateDefinition.Add(update.Set(c => c.Description, updateCategoryDto.Description));

        if (updateCategoryDto.Filters != null && updateCategoryDto.Filters.Any())
            updateDefinition.Add(update.Set(c => c.Filters, updateCategoryDto.Filters));

        if (updateCategoryDto.IconFile != null)
        {
            var iconFilePath =  SaveIconFile(updateCategoryDto.IconFile);
            updateDefinition.Add(update.Set(c => c.Icon.Path, iconFilePath.filePath));
            updateDefinition.Add(update.Set(c => c.Icon.Filename, iconFilePath.name));

        }

        if (updateDefinition.Any())
        {
            var combinedUpdate = update.Combine(updateDefinition);
            var result = await _dbContext.Categories.UpdateOneAsync(filter, combinedUpdate);

            return result.ModifiedCount > 0;
        }

        return false;
    }
    private bool IsSupportedImageFormat(string contentType)
    {
        var supportedFormats = new[] { "image/jpeg", "image/png", "image/gif" };
        return supportedFormats.Contains(contentType);
    }

    private (string filePath,string name) SaveIconFile(IFormFile iconFile)
    {
        string uuid = Guid.NewGuid().ToString();
        var fileName = $"{uuid}{Path.GetExtension(iconFile.FileName)}";
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../../online shop/online shop/Public/images/Categories");
        var filePath = Path.Combine(directoryPath, fileName);
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                iconFile.CopyTo(stream);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("An error occurred while saving the file.", ex);
        }

        return (filePath,fileName);
    }
    
}