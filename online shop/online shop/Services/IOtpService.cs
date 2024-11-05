using online_shop.DTO;

namespace online_shop.Services;

public interface IOtpService
{
     Task<string> GenerateOtp(string phone, int length = 4, int expireTime = 1);
}