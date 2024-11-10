using MongoDB.Bson;
using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;
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
    
    public async Task<bool> UpdateSellerAsync(ObjectId sellerId, AddSellerDto addSellerDto)
    {
        var filter = Builders<Seller>.Filter.Eq(s => s.Id, sellerId);

        var updateDefinition = new List<UpdateDefinition<Seller>>();

        if (!string.IsNullOrEmpty(addSellerDto.Name))
            updateDefinition.Add(Builders<Seller>.Update.Set(s => s.Name, addSellerDto.Name));

        if (!string.IsNullOrEmpty(addSellerDto.phone))
            updateDefinition.Add(Builders<Seller>.Update.Set(s => s.ContactDetails.Phone, addSellerDto.phone));

        if (!string.IsNullOrEmpty(addSellerDto.CityId))
        {
            updateDefinition.Add(Builders<Seller>.Update.Set(s => s.CityId, int.Parse(addSellerDto.CityId)));
        }
        if (updateDefinition.Any())
        {
            var combinedUpdate = Builders<Seller>.Update.Combine(updateDefinition);
            var result = await _dbContext.Sellers.UpdateOneAsync(filter, combinedUpdate);
            return result.ModifiedCount > 0;
        }

        return false;
    }
}