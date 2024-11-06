using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MongoDbContext _dbContext;

    public UserRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUsersCountAsync()
    {
        return (int)await _dbContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
    }

    public async Task<User> GetUserByPhoneAsync(string phone)
    {
        return await _dbContext.Users.Find(u=>u.Phone == phone).FirstOrDefaultAsync();
    }
    
    public async Task<string> AddUserAsync(User user)
    {
        await _dbContext.Users.InsertOneAsync(user);
        return user.Id.ToString();
    }
    
}