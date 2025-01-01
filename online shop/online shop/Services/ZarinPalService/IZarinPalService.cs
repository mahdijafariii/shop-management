using online_shop.DTO;

namespace online_shop.Services;

public interface IZarinPalService
{
    Task<PaymentResponseDto> CreatePayment(string userId,ShippingAddressDto shippingAddressDto);
}