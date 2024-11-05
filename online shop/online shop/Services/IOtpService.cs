using online_shop.DTO;

namespace online_shop.Services;

public interface IOtpService
{
    Task<OtpDetails> GetOtpDetailsAsync(string phone);
}