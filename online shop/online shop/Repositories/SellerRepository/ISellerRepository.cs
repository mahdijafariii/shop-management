using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories.SellerRepository;

public interface ISellerRepository
{
    Task<string> AddSellerAsync(Seller seller);
    Task<bool> CheckUserSeller(ObjectId user);
    Task<bool> UpdateSellerAsync(ObjectId sellerId, AddSellerDto addSellerDto);
    Task<bool> CheckStoreHasSeller(ObjectId seller);
    Task<bool> DeleteSellerAsync(ObjectId seller);


}