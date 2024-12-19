using online_shop.Model;

namespace online_shop.Repositories.SellerRequestRepository;

public interface ISellerRequestRepository
{
    Task<string> AddSellerRequestAsync(SellerRequest sellerRequest);
    Task<SellerRequest> IsRequestExistAsync(string sellerId, string productId);


}