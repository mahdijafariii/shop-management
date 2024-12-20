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

    public async Task<SellerRequest> GetNoteAsync(string noteId)
    {
        var result = await _dbContext.SellerRequest.Find(s => s.Id == noteId)
            .FirstOrDefaultAsync();
        return result !=null ? result : null;
    }

    public async Task<bool> DeleteNoteAsync(string sellerRequestId)
    {
        var filter = Builders<SellerRequest>.Filter.Eq(c => c.Id, sellerRequestId);
        var result = await _dbContext.SellerRequest.FindOneAndDeleteAsync(filter);
        return result != null;
    }

    public async Task<List<SellerRequest>> GetAllRequestAsync(string userId, int page, int limit)
    {
        var skip = (page - 1) * limit;

        var result = await _dbContext.SellerRequest.Find(p => p.SellerId == userId).Skip(skip)
            .Limit(limit).ToListAsync();
        if (result is null)
        {
            return null;
        }
        return result;
        
    }

    public async Task<int> SellerRequestTotalCount()
    {
        var total =(int) await _dbContext.SellerRequest.CountDocumentsAsync(FilterDefinition<SellerRequest>.Empty);
        return total;
    }
}