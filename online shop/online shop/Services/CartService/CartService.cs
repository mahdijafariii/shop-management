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

    public async Task<List<CartItem>> AddToCart(AddToCartDto request, string userId)
    {
        var product = await _productRepository.GetProductAsync(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }
        var seller = await _sellerRepository.GetSellerWithOutDtoAsync(request.SellerId);
        if (seller is null)
        {
            throw new NotFoundException("Seller");
        }
        
        var productSeller = await _productRepository.IsProductSeller(request.ProductId, request.SellerId);
        if (productSeller is null)
        {
            throw new InvalidRequestException("This seller dose not sell this product",400);
        }
        var cart = await _cartRepository.GetCart(userId);
        if (cart is null)
        {
            var cartItem = new CartItem()
            {
                ProductId = product.Id,
                SellerId = seller.Id,
                PriceAtTimeOfAdding = productSeller.Price,
                Quantity = request.Quantity
            };
            var checkStock = await _productRepository.HasSufficientStock(product.Id, seller.Id, request.Quantity);
            if (!checkStock)
            {
                throw new InvalidRequestException("You can not buy this product from seller with this count", 400);
            }
            var newCart = new Cart()
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Items = new List<CartItem>() { cartItem }
            };
            await _cartRepository.CreateCart(newCart);
            return newCart.Items;
        }

        var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
        if (productInCart is null)
        {
            var cartItem = new CartItem()
            {
                ProductId = product.Id,
                SellerId = seller.Id,
                PriceAtTimeOfAdding = productSeller.Price,
                Quantity = request.Quantity
            };
            var checkStock = await _productRepository.HasSufficientStock(product.Id, seller.Id, request.Quantity);
            if (!checkStock)
            {
                throw new InvalidRequestException("You can not buy this product from seller with this count", 400);
            }
            cart.Items.Add(cartItem);
            var newCart = await _cartRepository.AddProductToCartAsync(cart.Items, userId);
            return newCart;

        }
        else
        {
            var count = productInCart.Quantity;
            productInCart.Quantity = count + request.Quantity;
            var checkStock = await _productRepository.HasSufficientStock(product.Id, seller.Id, productInCart.Quantity);
            if (!checkStock)
            {
                throw new InvalidRequestException("You can not buy this product from seller with this count", 400);
            }
            var newCart = await _cartRepository.AddProductToCartAsync(cart.Items, userId);
            return newCart;
        }
    }

    public async Task<List<CartItem>> DeleteFromCart(DeleteFromCartDto request, string userId)
    {
        var product = await _productRepository.GetProductAsync(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }
        var productSeller = await _productRepository.IsProductSeller(request.ProductId, request.SellerId);
        if (productSeller is null)
        {
            throw new InvalidRequestException("This seller dose not sell this product",400);
        }
        var cart = await _cartRepository.GetCart(userId);
        Console.WriteLine("Cart: " + (cart == null ? "null" : "not null"));
        if (cart is null)
        {
            throw new NotFoundException("Cart");
        }
        var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId && x.SellerId == request.SellerId);
        if (productInCart is null)
        {
            throw new InvalidRequestException("Not found product with this information in you repo",400);
        }
        var count = productInCart.Quantity;
        productInCart.Quantity = count - 1;
        var items = await _cartRepository.DeleteProductFromCart(cart.Items, userId);
        return items;
    }

    public async Task<List<CartItem>> GetCart(string userId)
    {
        var cart = await _cartRepository.GetCart(userId);
        if (cart is null)
        {
            throw new InvalidRequestException("You have not add any product in your cart yet",400);
        }
        return cart.Items;
    }
}