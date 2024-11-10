using online_shop.Data;

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
}