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
    Task DeleteUserAddressAsync(ObjectId userId, ObjectId addressId);
}