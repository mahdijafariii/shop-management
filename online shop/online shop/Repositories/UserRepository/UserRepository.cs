using MongoDB.Bson;
using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MongoDbContext _dbContext;

    public UserRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUsersCountAsync()
    {
        return (int)await _dbContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
    }

    public async Task<User> GetUserByPhoneAsync(string phone)
    {
        return await _dbContext.Users.Find(u => u.Phone == phone).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        ObjectId.TryParse(id, out var objectId);
        return await _dbContext.Users.Find(u => u.Id == objectId).FirstOrDefaultAsync();
    }

    public async Task<string> AddUserAsync(User user)
    {
        await _dbContext.Users.InsertOneAsync(user);
        return user.Id.ToString();
    }

    public async Task AddUserAddressAsync(Address address, ObjectId id)
    {
        var updateDefinition = Builders<User>.Update.Push(u => u.Addresses, address);
        await _dbContext.Users.FindOneAndUpdateAsync(u => u.Id == id, updateDefinition);
    }


    public async Task<bool> DeleteUserAddressAsync(ObjectId userId, ObjectId addressId)
    {
        var updateDefinition = Builders<User>.Update.PullFilter(u => u.Addresses, a => a.Id == addressId);
        var result = await _dbContext.Users.UpdateOneAsync(u => u.Id == userId, updateDefinition);

        if (result.ModifiedCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<(List<User> Users, long TotalCount)> GetAllUsersAsync(int page , int limit)
    {
        var skip = (page - 1) * limit;

        var totalCount = await _dbContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);

        var users = await _dbContext.Users
            .Find(FilterDefinition<User>.Empty)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
        return (users, totalCount);
    }

    public async Task<bool> UpdateAddressAsync(ObjectId userId, ObjectId addressId, UpdateAddressDto updateAddressDto)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.Id, userId),
            Builders<User>.Filter.ElemMatch(u => u.Addresses, a => a.Id == addressId)
        );

        var update = Builders<User>.Update;
        var updateDefinition = new List<UpdateDefinition<User>>();

        if (!string.IsNullOrEmpty(updateAddressDto.Name))
            updateDefinition.Add(update.Set(u => u.Addresses[-1].Name, updateAddressDto.Name));

        if (!string.IsNullOrEmpty(updateAddressDto.PostalCode))
            updateDefinition.Add(update.Set(u => u.Addresses[-1].PostalCode, updateAddressDto.PostalCode));

        if (updateAddressDto.Lat.HasValue)
            updateDefinition.Add(update.Set(u => u.Addresses[-1].Lat, updateAddressDto.Lat.Value));

        if (updateAddressDto.Lng.HasValue)
            updateDefinition.Add(update.Set(u => u.Addresses[-1].Lng, updateAddressDto.Lng.Value));

        if (!string.IsNullOrEmpty(updateAddressDto.AddressLine))
            updateDefinition.Add(update.Set(u => u.Addresses[-1].AddressLine, updateAddressDto.AddressLine));

        if (updateAddressDto.CityId.HasValue)
            updateDefinition.Add(update.Set(u => u.Addresses[-1].CityId, updateAddressDto.CityId.Value));

        if (updateDefinition.Any())
        {
            var combinedUpdate = update.Combine(updateDefinition);
            var result = await _dbContext.Users.UpdateOneAsync(filter, combinedUpdate);

            if (result.ModifiedCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}