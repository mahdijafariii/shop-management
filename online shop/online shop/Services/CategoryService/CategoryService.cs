using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Repositories.CategoryRepository;

namespace online_shop.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        (string, string) icon = (null, null);
        if (createCategoryDto.IconFile != null)
        {
            if (!IsSupportedImageFormat(createCategoryDto.IconFile.ContentType))
            {
                throw new InvalidRequestException("Unsupported image format !!!" , StatusCodes.Status400BadRequest);
            }

            icon = SaveIconFile(createCategoryDto.IconFile);
        }

        var category = new Category
        {
            Title = createCategoryDto.Title,
            Slug = createCategoryDto.Slug,
            ParentId = createCategoryDto.Parent,
            Description = createCategoryDto.Description,
            Icon = new Icon(){Filename = icon.Item2 , Path = icon.Item1},
            Filters = createCategoryDto.Filters
        };

        return await _categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<SubCategory> CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto)
    {
        var checkParent = await _categoryRepository.CheckParentIdValidator(createSubCategoryDto.Parent);
        if (!checkParent)
        {
            throw new NotFoundException("Parent category");
        }
        var category = new SubCategory()
        {
            Title = createSubCategoryDto.Title,
            Slug = createSubCategoryDto.Slug,
            ParentId = createSubCategoryDto.Parent.ToString(),
            Description = createSubCategoryDto.Description,
            Filters = createSubCategoryDto.Filters
        };

        return await _categoryRepository.CreateSubCategoryAsync(category);
        
    }

    public async Task UpdateCategoryAsync(string categoryId ,UpdateCategoryDto updateCategoryDto)
    {
        (string, string) icon = (null, null);
        if (updateCategoryDto.IconFile != null)
        {
            if (!IsSupportedImageFormat(updateCategoryDto.IconFile.ContentType))
            {
                throw new InvalidRequestException("Unsupported image format !!!" , StatusCodes.Status400BadRequest);
            }

            icon = SaveIconFile(updateCategoryDto.IconFile);
        }

        var result = await _categoryRepository.UpdateCategoryAsync(categoryId,updateCategoryDto);
        if (!result)
        {
            throw new InvalidCastException("Does not update successfully !!");
        }
    }

    public async Task DeleteCategoryAsync(string objectId)
    {
        if (!ObjectId.TryParse(objectId, out var categoryId))
        {
            throw new ArgumentException("Invalid category ID format.");
        }
        var result = await _categoryRepository.DeleteCategoryAsync(objectId);
        if (result)
        {
            throw new NotFoundException("Category");
        }
    }

    public async Task<List<SubCategory>> GetALlSubCategories()
    {
        var result = await _categoryRepository.GetALlSubCategories();
        return result;
    }

    public Task<List<SubCategory>> GetSubCategories(string categoryId)
    {
        var result = _categoryRepository.GetSubCategories(categoryId);
        return result;
    }

    public async Task DeleteSubCategories(string categoryId)
    {
        var result = await _categoryRepository.DeleteSubCategories(categoryId);
        if (result)
        {
            throw new NotFoundException("Category");
        }
    }

    public async Task UpdateSubCategoryAsync(string subCategoryId, UpdateSubCategoryDto updateSubCategoryDto)
    {
        var result = await _categoryRepository.UpdateSubCategoryAsync(subCategoryId, updateSubCategoryDto);
        if (!result)
        {
            throw new InvalidRequestException("reqeust was not succesffull",400);
        }
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
    
    
    public async Task<List<Category>> FetchAllCategoriesAsync()
    {
        try
        {
            var categories = await FetchCategoriesRecursivelyAsync(null);
            return categories;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("Error fetching categories", ex);
        }
    }

    private async Task<List<Category>> FetchCategoriesRecursivelyAsync(string parentId)
    {
        ObjectId? parentObjectId = string.IsNullOrEmpty(parentId) ? null : ObjectId.Parse(parentId);

        var parentCategories = await _categoryRepository.GetCategoriesSub(parentObjectId); // level 1 categories

        var fetchedCategories = new List<Category>();

        foreach (var category in parentCategories)
        {
            category.SubCategories = await FetchSubCategoriesForCategoryAsync(category.Id); // level 2 categories

            fetchedCategories.Add(category);
        }

        return fetchedCategories;
    }

    private async Task<List<SubCategory>> FetchSubCategoriesForCategoryAsync(string parentId)
    {
        var subCategories = await _categoryRepository.GetSubCategories(parentId);

        var fetchedSubCategories = new List<SubCategory>();

        foreach (var subCategory in subCategories)
        {
            fetchedSubCategories.Add(subCategory); // level 3 categories
        }

        return fetchedSubCategories;
    }
    
}