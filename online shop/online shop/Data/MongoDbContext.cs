using MongoDB.Driver;
using online_shop.Model;

namespace online_shop.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase("shop");
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<BanUser> BanUsers => _database.GetCollection<BanUser>("BanUsers");
    public IMongoCollection<Address> Addresses => _database.GetCollection<Address>("Addresses");

}