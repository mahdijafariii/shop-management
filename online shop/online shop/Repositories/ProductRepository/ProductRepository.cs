using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly MongoDbContext _dbContext;

    public ProductRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> IsShortIdentifierExistsAsync(string id)
    {
        var filter = Builders<Seller>.Filter.Eq(c => c.Id, id);
        var product = await _dbContext.Sellers.Find(filter).FirstOrDefaultAsync();
        return product != null;
    }
}