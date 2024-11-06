using online_shop.DTO;

namespace online_shop.Services;

public interface IAuthService
{
    Task<VerifyUserDto> VerifyOtpAndAuthUser(string phone, string otp, bool isSeller);
}