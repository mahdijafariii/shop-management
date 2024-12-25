using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories.SellerRepository;

public interface ISellerRepository
{
    Task<string> AddSellerAsync(Seller seller);
    Task<bool> CheckUserSeller(ObjectId user);
    Task<bool> UpdateSellerAsync(string sellerId, AddSellerDto addSellerDto);
    Task<bool> CheckStoreHasSeller(string seller);
    Task<bool> DeleteSellerAsync(string seller);
    Task<(GetSellerDto, bool)> GetSellerAsync(string sellerId);
    Task<Seller> GetSellerWithOutDtoAsync(string sellerId);

}