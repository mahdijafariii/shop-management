using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface ICartService
{
    Task<Cart> AddToCart(AddToCartDto request, string userId);

}