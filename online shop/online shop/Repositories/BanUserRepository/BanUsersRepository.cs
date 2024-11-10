using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories;

public class BanUsersRepository : IBanUsersRepository
{
    private readonly MongoDbContext _dbContext;

    public BanUsersRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> IsUserBannedAsync(string phone)
    {
        return await _dbContext.BanUsers.Find(u => u.Phone == phone).AnyAsync();
    }
    
    public async Task AddToBanedUsersAsync(string phone)
    {
        var banUser = new BanUser() { Phone = phone };
        await _dbContext.BanUsers.InsertOneAsync(banUser);
    }
}