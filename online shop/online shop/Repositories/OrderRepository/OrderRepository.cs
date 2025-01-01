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

    public async Task<Order> AddOrderAsync(Order order)
    {
        await _dbContext.Order.InsertOneAsync(order);
        return order;
    }

    public async Task<List<Order>> GetAllOrdersAsync(int page, int limit)
    {
        var skip = (page - 1) * limit;

        var result = await _dbContext.Order.Find(FilterDefinition<Order>.Empty).Skip(skip)
            .Limit(limit).ToListAsync();
        if (result is null)
        {
            return null;
        }
        return result;
    }

    public async Task<List<Order>> GetAllUserOrdersAsync(string userId, int page, int limit)
    {
        var skip = (page - 1) * limit;

        var result = await _dbContext.Order.Find(p => p.UserId == userId).Skip(skip)
            .Limit(limit).ToListAsync();
        if (result is null)
        {
            return null;
        }
        return result;
    }
    public async Task<int> OrdersTotalCount()
    {
        var totalCount = await _dbContext.Order.CountDocumentsAsync(FilterDefinition<Order>.Empty);
        return (int)totalCount;
    }

    public async Task<int> OrdersOfUserTotalCount(string userId)
    {
        var totalCount = await _dbContext.Order.CountDocumentsAsync(p => p.UserId == userId);
        return (int)totalCount;
    }
}