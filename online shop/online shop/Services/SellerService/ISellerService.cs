using MongoDB.Bson;
using online_shop.DTO;

namespace online_shop.Services.SellerService;

public interface ISellerService
{
    Task<string> AddSellerAsync(ObjectId user,AddSellerDto addSeller);
}