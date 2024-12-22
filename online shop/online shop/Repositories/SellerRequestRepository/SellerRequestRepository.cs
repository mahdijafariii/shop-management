using MongoDB.Driver;
using Newtonsoft.Json;
using online_shop.Data;
using online_shop.DTO;
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
        return result != null ? result : null;
    }

    public async Task<SellerRequest> GetSellerRequestAsync(string noteId)
    {
        var result = await _dbContext.SellerRequest.Find(s => s.Id == noteId)
            .FirstOrDefaultAsync();
        return result != null ? result : null;
    }

    public async Task<bool> DeleteNoteAsync(string sellerRequestId)
    {
        var filter = Builders<SellerRequest>.Filter.Eq(c => c.Id, sellerRequestId);
        var result = await _dbContext.SellerRequest.FindOneAndDeleteAsync(filter);
        return result != null;
    }

    public async Task<List<SellerRequest>> GetAllRequestAsync(string userId, int page, int limit, string status)
    {
        var skip = (page - 1) * limit;
        var statusLower = status.ToLower();

        var result = await _dbContext.SellerRequest.Find(p => p.SellerId == userId && p.Status.ToLower() == statusLower)
            .Skip(skip)
            .Limit(limit).ToListAsync();
        if (result is null || !result.Any())
        {
            return null;
        }

        return result;

    }

    public async Task<int> SellerRequestTotalCount()
    {
        var total = (int)await _dbContext.SellerRequest.CountDocumentsAsync(FilterDefinition<SellerRequest>.Empty);
        return total;
    }

    public async Task<bool> UpdateSellerRequestAsync(UpdateSellerRequestDto requestDto)
    {
        var filter = Builders<SellerRequest>.Filter.And(
            Builders<SellerRequest>.Filter.Eq(s => s.Id, requestDto.RequestId)
        );
        var updateDefinition = new List<UpdateDefinition<SellerRequest>>();

        if (!string.IsNullOrEmpty(requestDto.AdminComment))
            updateDefinition.Add(Builders<SellerRequest>.Update.Set(s => s.AdminComment, requestDto.AdminComment));

        if (!string.IsNullOrEmpty(requestDto.Status))
            updateDefinition.Add(Builders<SellerRequest>.Update.Set(s => s.Status, requestDto.Status));

        if (updateDefinition.Any())
        {
            var combinedUpdate = Builders<SellerRequest>.Update.Combine(updateDefinition);
            var result = await _dbContext.SellerRequest.UpdateOneAsync(filter, combinedUpdate);
            return result.ModifiedCount > 0;
        }

        return false;
    }

    public async Task<bool> UpdateProductSellersAsync(string productId, List<ProductSeller> sellers)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
        var sellersJson = sellers.Select(seller => JsonConvert.SerializeObject(seller)).ToList();
        var update = Builders<Product>.Update.Set<List<string>>(p => p.Sellers, sellersJson);

        var result = await _dbContext.Product.UpdateOneAsync(filter, update);

        if (result.MatchedCount == 0)
        {
            return false;
        }

        return true;
    }
}