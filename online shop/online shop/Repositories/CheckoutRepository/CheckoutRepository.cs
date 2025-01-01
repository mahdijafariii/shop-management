using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly MongoDbContext _dbContext;

    public CheckoutRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Checkout> AddCheckoutAsync(Checkout checkout)
    {
        var indexKeys = Builders<Checkout>.IndexKeys.Ascending(c => c.ExpiresAt);
        var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
        var indexModel = new CreateIndexModel<Checkout>(indexKeys, indexOptions);
        await _dbContext.Checkout.InsertOneAsync(checkout);
        return checkout;
    }
}