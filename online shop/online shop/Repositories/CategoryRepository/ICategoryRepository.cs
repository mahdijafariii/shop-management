namespace online_shop.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
}