using MongoDB.Driver;

namespace online_shop.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase("shop");
    }

    // public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}