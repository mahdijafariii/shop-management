using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services.ProductService;

public interface IProductService
{
    Task CreateProduct(CreateProductDto model);
    Task DeletesProduct(string request);
    Task<Product> GetProduct(string productId);
    Task<Product> GetProductWithIdentifier(string shortIdentifier);
    Task UpdateProduct(UpdateProduct request);
    Task<List<Product>> GetProductsAsync(ProductFilterDto filterDto);
}