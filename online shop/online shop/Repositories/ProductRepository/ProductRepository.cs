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
    public async Task<Product> DeleteProductAsync(string productId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();

        if (product != null)
        {
            var result = await _dbContext.Product.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
            {
                return product; 
            }
        }
    
        return null;
    }

    public async Task<Product> GetProductAsync(string productId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();
        if (product != null)
        {
            var subCategory = await _dbContext.SubCategories
                .Find(s => s.Id == product.SubCategoryId)
                .FirstOrDefaultAsync();
            product.SubCategory = subCategory;
        }
        return product != null ? product : null;
    }

    public async Task<Product> IsProductExist(string productId)
    {
        var result = await _dbContext.Product.Find(p => p.Id == productId).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _dbContext.Product.InsertOneAsync(product);
        return product;
    }
}