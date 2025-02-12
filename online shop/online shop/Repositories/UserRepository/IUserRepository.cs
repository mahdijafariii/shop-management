using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories;

public interface IUserRepository
{
    Task<int> GetUsersCountAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<User> GetUserByPhoneAsync(string phone);
    Task<string> AddUserAsync(User user);
    Task AddUserAddressAsync(Address address, ObjectId id);
    Task<bool> DeleteUserAddressAsync(ObjectId userId, ObjectId addressId);
    Task<bool> UpdateAddressAsync(ObjectId userId, ObjectId addressId, UpdateAddressDto updateAddressDto);
    Task<(List<User> Users, long TotalCount)> GetAllUsersAsync(int page, int limit);
}