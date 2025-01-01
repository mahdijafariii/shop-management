using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MongoDbContext _dbContext;

    public OrderRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> GetOrderAsync(string authority)
    {
        var result = await _dbContext.Order.Find(p => p.Authority == authority).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        return result;
    }
}