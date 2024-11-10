using System.Security.Claims;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface IAuthService
{
    Task<VerifyUserDto> VerifyOtpAndAuthUser(string phone, string otp, bool isSeller);
    Task<GetMeDto> GetMe(ClaimsPrincipal claimsPrincipal);

}