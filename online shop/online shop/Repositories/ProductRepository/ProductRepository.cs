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
    public async Task<bool> DeleteProductAsync(string productId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId
        );
        var result = await _dbContext.Product.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _dbContext.Product.InsertOneAsync(product);
        return product;
    }
}