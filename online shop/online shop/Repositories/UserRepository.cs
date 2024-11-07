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
    
    public async Task<AddressDto> AddUserAddressAsync(Address address,User user)
    {
        var updateDefinition = Builders<User>.Update.Push(u => u.Addresses, address); 
        await _dbContext.Users.FindOneAndUpdateAsync(u => u.Id == user.Id, updateDefinition);
        
        return new AddressDto(user.Addresses);
    }
    
}