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
        await _dbContext.Checkout.InsertOneAsync(checkout);
        return checkout;
    }

    public async Task<Checkout> GetCheckoutAsync(string authority)
    {

        var result = await _dbContext.Checkout.Find(p => p.Authority == authority).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }
        return result;
    }
}