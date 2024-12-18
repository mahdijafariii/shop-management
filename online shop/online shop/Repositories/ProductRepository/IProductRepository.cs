using online_shop.Model;

namespace online_shop.Repositories.ProductRepository;

public interface IProductRepository
{
    Task<bool> IsShortIdentifierExistsAsync(string id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> DeleteProductAsync(string productId);
    Task<Product> GetProductAsync(string productId);

}