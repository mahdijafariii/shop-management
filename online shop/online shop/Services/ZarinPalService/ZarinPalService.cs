using System.Text;
using System.Text.Json;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;
using online_shop.Repositories.ProductRepository;
using online_shop.Repositories.SellerRepository;

namespace online_shop.Services;

public class ZarinPalService : IZarinPalService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _callBackUrl;
    private readonly string _merchantId;
    private readonly IUserRepository _userRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISellerRepository _sellerRepository;
    private readonly ICheckoutService _checkoutService;
    
    public ZarinPalService(HttpClient httpClient, IConfiguration configuration, IUserRepository userRepository, ICartRepository cartRepository, IProductRepository productRepository, ISellerRepository sellerRepository, ICheckoutService checkoutService)
    {
        _httpClient = httpClient;
        _userRepository = userRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _sellerRepository = sellerRepository;
        _checkoutService = checkoutService;
        var zarinPalSettings = configuration.GetSection("ZarinPal");
        _baseUrl = zarinPalSettings["BaseUrl"];
        _callBackUrl = zarinPalSettings["CallBackUrl"];
        _merchantId = zarinPalSettings["MerchantId"];

    }

    public async Task<PaymentResponseDto> CreatePayment(string userId, ShippingAddressDto shippingAddressDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var cart = await _cartRepository.GetCart(userId);
        if (cart is null || !cart.Items.Any())
        {
            throw new InvalidRequestException("You have not any product in your cart",400);
        }
        var productInCart = new List<CartItem>();
        var cartPrice = 0.0;
        foreach (var product in cart.Items)
        {
            var sellerSellIt = await _productRepository.IsProductSeller(product.ProductId, product.SellerId);
            if (sellerSellIt is null)
            {
                throw new InvalidRequestException("This seller dose not sell this product",400);
            }
            productInCart.Add(product);
            cartPrice = cartPrice + product.PriceAtTimeOfAdding;
        }
        
        var requestBody = new
        {
            merchant_id = _merchantId,
            callback_url = _callBackUrl,
            description = "خرید کالا از سایت فروشگاهی ما",
            amount = cartPrice,
        };
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_baseUrl, content);
        if(!response.IsSuccessStatusCode)
        {
            throw new InvalidRequestException("Failed to create payment.",400);
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        JsonDocument doc = JsonDocument.Parse(responseContent);
        JsonElement root = doc.RootElement;
        var authority = root.GetProperty("data").GetProperty("authority").GetString();
        ShippingAddress shippingAddress = new ShippingAddress()
        {
            Address = shippingAddressDto.Address,
            CityId = shippingAddressDto.CityId,
            PostalCode = shippingAddressDto.PostalCode,
            Coordinates = new Coordinates() { Lat = shippingAddressDto.Lat, Lng = shippingAddressDto.Lng }
        };
        var checkout = new Checkout()
        {
            UserId = userId,
            ShippingAddress = shippingAddress,
            Items = productInCart,
            Authority = authority
        };
        await _checkoutService.AddCheckout(checkout);
        var paymentUrl = "https://sandbox.zarinpal.com/pg/StartPay/";
        return new PaymentResponseDto(authority, $"{paymentUrl}{authority}");
    }
}