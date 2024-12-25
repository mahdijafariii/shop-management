using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;
using online_shop.Repositories.ProductRepository;
using online_shop.Repositories.SellerRepository;

namespace online_shop.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISellerRepository _sellerRepository;


    public CartService(ICartRepository cartRepository, IProductRepository productRepository, ISellerRepository sellerRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<Cart> AddToCart(AddToCartDto request, string userId)
    {
        // check product hast ya na
        var product = await _productRepository.GetProductAsync(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }
        // check seller seller hast ya na
        var seller = await _sellerRepository.GetSellerWithOutDtoAsync(request.SellerId);
        if (seller is null)
        {
            throw new NotFoundException("Seller");
        }
        
        // check sellere kala hast ya na
        
        // check sabad dare ya na
        
        // check kala to sabad hast ya na
        
        throw new NotImplementedException();
    }
}