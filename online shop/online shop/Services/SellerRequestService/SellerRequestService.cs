using online_shop.DTO;
using online_shop.Model;
using online_shop.Repositories.SellerRequestRepository;

namespace online_shop.Services;

public class SellerRequestService : ISellerRequestService
{
    private readonly ISellerRequestRepository _sellerRequestRepository;

    public SellerRequestService(ISellerRequestRepository sellerRequestRepository)
    {
        _sellerRequestRepository = sellerRequestRepository;
    }

    public async Task<string> CreateRequest(string sellerId, CreateSellerRequest request)
    {
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