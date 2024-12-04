using online_shop.Model;

namespace online_shop.Repositories.ProductRepository;

public interface IProductRepository
{
    Task<bool> IsShortIdentifierExistsAsync(string id);
    Task AddProductAsync(Product product);
}