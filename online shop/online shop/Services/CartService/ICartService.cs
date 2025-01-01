using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface ICartService
{
    Task<List<CartItem>> AddToCart(AddToCartDto request, string userId);
    Task<List<CartItem>> DeleteFromCart(DeleteFromCartDto request, string userId);
    Task<List<CartItem>> GetCart(string userId);
    Task DeleteCart(string userId);
}