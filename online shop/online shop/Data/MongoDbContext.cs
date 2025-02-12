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
    public IMongoCollection<Seller> Sellers => _database.GetCollection<Seller>("Sellers");
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    public IMongoCollection<SubCategory> SubCategories => _database.GetCollection<SubCategory>("SubCategories");
    public IMongoCollection<Product> Product => _database.GetCollection<Product>("Product");
    public IMongoCollection<Note> Note => _database.GetCollection<Note>("Note");
    public IMongoCollection<SellerRequest> SellerRequest => _database.GetCollection<SellerRequest>("SellerRequest");
    public IMongoCollection<Comment> Comment => _database.GetCollection<Comment>("Comment");
    public IMongoCollection<Cart> Cart => _database.GetCollection<Cart>("Cart");
    public IMongoCollection<Checkout> Checkout => _database.GetCollection<Checkout>("Checkout");
    public IMongoCollection<Order> Order => _database.GetCollection<Order>("Order");

    public async Task ConfigureIndexesAsync()
    {
        var indexKeys = Builders<Checkout>.IndexKeys.Ascending(c => c.ExpiresAt);
        var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
        var indexModel = new CreateIndexModel<Checkout>(indexKeys, indexOptions);

        await Checkout.Indexes.CreateOneAsync(indexModel);
    }
}