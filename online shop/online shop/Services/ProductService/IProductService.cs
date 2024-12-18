using online_shop.DTO;

namespace online_shop.Services.ProductService;

public interface IProductService
{
    Task CreateProduct(CreateProductDto model);
    Task DeletesProduct(string request);

}