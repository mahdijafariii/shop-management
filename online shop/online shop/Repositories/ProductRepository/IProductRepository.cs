using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories.ProductRepository;

public interface IProductRepository
{
    Task<bool> IsShortIdentifierExistsAsync(string id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> DeleteProductAsync(string productId);
    Task<Product> GetProductAsync(string productId);
    Task<Product> IsProductExist(string productId);
    Task<Product> GetProductWithIdentifier(string shortIdentifier);
    Task<bool> UpdateProductAsync(UpdateProduct request);
    Task<ProductSeller> IsProductSeller(string productId,string sellerId);

}