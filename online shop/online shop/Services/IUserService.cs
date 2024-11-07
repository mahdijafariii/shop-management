using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface IUserService
{
    Task AddAddressAsync(AddressDto addressDto, ObjectId id);
}