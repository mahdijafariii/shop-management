using online_shop.DTO;

namespace online_shop.Services;

public interface ISellerRequestService
{
    Task<string> CreateRequest(string sellerId,CreateSellerRequest request);
    Task DeleteSellerRequest(string requestId, string userId);
}