using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories.SellerRequestRepository;

public class SellerRequestRepository : ISellerRequestRepository
{
    private readonly MongoDbContext _dbContext;

    public SellerRequestRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> AddSellerRequestAsync(SellerRequest sellerRequest)
    {
        await _dbContext.SellerRequest.InsertOneAsync(sellerRequest);
        return sellerRequest.Id;
    }

    public async Task<SellerRequest> IsRequestExistAsync(string sellerId, string productId)
    {
        var result = await _dbContext.SellerRequest.Find(s => s.SellerId == sellerId && s.ProductId == productId)
            .FirstOrDefaultAsync();
        return result !=null ? result : null;
    }
}