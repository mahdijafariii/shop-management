using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories.SellerRepository;

public class SellerRepository : ISellerRepository
{
    private readonly MongoDbContext _dbContext;

    public SellerRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> AddSellerAsync(Seller seller)
    {
        await _dbContext.Sellers.InsertOneAsync(seller);
        return seller.Id.ToString();
    }
}