using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories.SellerRequestRepository;

public interface ISellerRequestRepository
{
    Task<string> AddSellerRequestAsync(SellerRequest sellerRequest);
    Task<SellerRequest> IsRequestExistAsync(string sellerId, string productId);
    Task<SellerRequest> GetSellerRequestAsync(string noteId);
    Task<bool> DeleteNoteAsync(string sellerRequestId);
    Task<List<SellerRequest>> GetAllRequestAsync(string userId, int page, int limit, string status);
    Task<int> SellerRequestTotalCount();
    Task<bool> UpdateSellerRequestRejectedAsync(UpdateSellerRequestDto requestDto);

}