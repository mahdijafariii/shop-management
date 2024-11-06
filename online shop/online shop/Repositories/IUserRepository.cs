using online_shop.Model;

namespace online_shop.Repositories;

public interface IUserRepository
{
    Task<int> GetUsersCountAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<User> GetUserByPhoneAsync(string phone);
    Task<string> AddUserAsync(User user);
}