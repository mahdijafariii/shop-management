using MongoDB.Bson;
using MongoDB.Driver;
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

    public async Task<bool> CheckUserSeller(ObjectId user)
    {
        var seller = await _dbContext.Sellers.Find(s => s.User == user).FirstOrDefaultAsync();
        return seller != null;
    }
}