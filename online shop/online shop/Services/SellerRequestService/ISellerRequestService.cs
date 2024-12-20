using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface ISellerRequestService
{
    Task<string> CreateRequest(string sellerId,CreateSellerRequest request);
    Task DeleteSellerRequest(string requestId, string userId);
    Task<(List<SellerRequest>, int totalCount)> GetAllRequest(string userId, int page , int limit);

}