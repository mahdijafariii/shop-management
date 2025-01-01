using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;

namespace online_shop.Repositories;

public class CartRepository : ICartRepository
{
    private readonly MongoDbContext _dbContext;
    

    public CartRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Cart> GetCart(string userId)
    {
        var result = await _dbContext.Cart.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }
        return result;
    }

    public async Task<bool> DeleteCart(string userId)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
        var result = await _dbContext.Cart.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
        
    }

    public async Task<Cart> CreateCart(Cart cart)
    {
        await _dbContext.Cart.InsertOneAsync(cart);
        return cart;
    }
    public async Task<List<CartItem>> AddProductToCartAsync(List<CartItem> cartItems,string userId)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
        var result = await _dbContext.Cart.Find(filter).FirstOrDefaultAsync();
        if (result.Items == null)
        {
            result.Items = new List<CartItem>();
        }
        var update = Builders<Cart>.Update.Set(c => c.Items, cartItems);
        await _dbContext.Cart.UpdateOneAsync(filter, update);
        return cartItems;
    }
    public async Task<List<CartItem>> DeleteProductFromCart(List<CartItem> cartItems, string userId)
    {
        var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
        var result = await _dbContext.Cart.Find(filter).FirstOrDefaultAsync();
        if (result is null)
        {
            throw new NotFoundException("Cart");
        }
        var update = Builders<Cart>.Update.Set(c => c.Items, cartItems);
        await _dbContext.Cart.UpdateOneAsync(filter, update);
        return cartItems;        
    }
}