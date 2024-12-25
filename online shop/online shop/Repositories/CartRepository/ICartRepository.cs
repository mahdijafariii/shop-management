using online_shop.Model;

namespace online_shop.Repositories;

public interface ICartRepository
{
    Task<Cart> GetCart(string userId);
    Task<Cart> CreateCart(Cart cart);
    Task<List<CartItem>> AddProductToCartAsync(List<CartItem> cartItems, string userId);


}