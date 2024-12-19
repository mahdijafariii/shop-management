using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories.ProductRepository;
using online_shop.Repositories.SellerRequestRepository;

namespace online_shop.Services;

public class SellerRequestService : ISellerRequestService
{
    private readonly ISellerRequestRepository _sellerRequestRepository;
    private readonly IProductRepository _productRepository;


    public SellerRequestService(ISellerRequestRepository sellerRequestRepository, IProductRepository productRepository)
    {
        _sellerRequestRepository = sellerRequestRepository;
        _productRepository = productRepository;
    }

    public async Task<string> CreateRequest(string sellerId, CreateSellerRequest request)
    {
        var product = await _productRepository.IsProductExist(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }

        var requestExistBefore = await _sellerRequestRepository.IsRequestExistAsync(sellerId, request.ProductId);
        if (requestExistBefore.Status == SellerRequestStatus.Pending.ToString())
        {
            throw new InvalidRequestException("You send request before please wait", 404);
        }
        var sellerRequest = new SellerRequest()
        {
            ProductId = request.ProductId,
            SellerId = sellerId,
            Stock = request.Stock,
            Price = request.Price,
        };
        var id= await _sellerRequestRepository.AddSellerRequestAsync(sellerRequest);
        return id;
    }
}