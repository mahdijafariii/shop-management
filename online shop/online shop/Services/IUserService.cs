using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface IUserService
{
    Task AddAddressAsync(AddAddressDto addAddressDto, ObjectId id);
    Task DeleteAddressAddressAsync(ObjectId addressId, ObjectId userId);
}