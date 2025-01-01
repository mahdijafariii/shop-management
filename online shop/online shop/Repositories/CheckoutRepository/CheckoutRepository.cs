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
        await _dbContext.Checkout.InsertOneAsync(checkout);
        return checkout;
    }
}