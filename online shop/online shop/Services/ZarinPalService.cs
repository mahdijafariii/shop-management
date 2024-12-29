using System.Text;
using System.Text.Json;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Repositories;

namespace online_shop.Services;

public class ZarinPalService : IZarinPalService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _callBackUrl;
    private readonly string _merchantId;
    private readonly IUserRepository _userRepository;
    
    public ZarinPalService(HttpClient httpClient, IConfiguration configuration, IUserRepository userRepository)
    {
        _httpClient = httpClient;
        _userRepository = userRepository;
        var zarinPalSettings = configuration.GetSection("ZarinPal");
        _baseUrl = zarinPalSettings["BaseUrl"];
        _callBackUrl = zarinPalSettings["CallBackUrl"];
        _merchantId = zarinPalSettings["MerchantId"];

    }

    public async Task<PaymentResponseDto> CreatePayment(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var requestBody = new
        {
            merchant_id = _merchantId,
            callback_url = _callBackUrl,
            description = "خرید کالا از سایت فروشگاهی ما",
            amount = 50000,
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
        var paymentUrl = "https://sandbox.zarinpal.com/pg/StartPay/";
        return new PaymentResponseDto(authority, $"{paymentUrl}{authority}");
    }
}