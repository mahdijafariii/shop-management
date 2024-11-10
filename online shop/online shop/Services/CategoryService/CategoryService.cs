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

    private bool IsSupportedImageFormat(string contentType)
    {
        var supportedFormats = new[] { "image/jpeg", "image/png", "image/gif" };
        return supportedFormats.Contains(contentType);
    }

    private (string filePath,string name) SaveIconFile(IFormFile iconFile)
    {
        string uuid = Guid.NewGuid().ToString();
        var fileName = Path.GetFileName(uuid);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Public/Images/Categories");
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            iconFile.CopyTo(stream);
        }

        return (filePath,fileName);
    }
    
}