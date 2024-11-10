using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface IUserService
{
    Task AddAddressAsync(AddAddressDto addAddressDto, ObjectId id);
    Task DeleteAddressAsync(ObjectId addressId, ObjectId userId);
    Task UpdateAddressAsync(ObjectId addressId, ObjectId userId, UpdateAddressDto updateAddressDto);
    Task<(List<User> Users, PaginationDynamicDto)> GetAllUsersAsync(int page = 1, int limit = 10);
}