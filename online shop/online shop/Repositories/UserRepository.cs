using MongoDB.Bson;
using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;
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
    
    public async Task<User> GetUserByIdAsync(string id)
    {
        ObjectId.TryParse(id,out var objectId);
        return await _dbContext.Users.Find(u=>u.Id == objectId).FirstOrDefaultAsync();
    }
    
    public async Task<string> AddUserAsync(User user)
    {
        await _dbContext.Users.InsertOneAsync(user);
        return user.Id.ToString();
    }
    
    public async Task AddUserAddressAsync(Address address,ObjectId id)
    {
        var updateDefinition = Builders<User>.Update.Push(u => u.Addresses, address); 
        await _dbContext.Users.FindOneAndUpdateAsync(u => u.Id == id, updateDefinition);
    }
    
    
    public async Task DeleteUserAddressAsync(ObjectId userId,ObjectId addressId)
    {
        var updateDefinition = Builders<User>.Update.PullFilter(u => u.Addresses,a=>a.Id == addressId); 
        await _dbContext.Users.UpdateOneAsync(u => u.Id == userId, updateDefinition);
    }
}