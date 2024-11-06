using System.Security.Claims;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;
using StackExchange.Redis;

namespace online_shop.Services;

public class AuthService : IAuthService
{
    private readonly IDatabase _redis;
    private readonly IUserRepository _userRepository;
    private readonly ICookieService _cookieService;
    private readonly IJwtService _jwtService;
    private readonly IOtpService _otpService;


    public AuthService(IJwtService jwtService, IUserRepository userRepository, ICookieService cookieService, IDatabase redis, IOtpService otpService)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _cookieService = cookieService;
        _redis = redis;
        _otpService = otpService;
    }


    public async Task<VerifyUserDto> VerifyOtpAndAuthUser(string phone, string otp ,bool isSeller)
    {
        var otpResult = await _redis.StringGetAsync(_otpService.GetOtpRedisPattern(phone));
        if (otpResult.IsNullOrEmpty)
        {
            throw new OtpInvalidException();
        }

        var correctOtp = BCrypt.Net.BCrypt.Verify(otp, otpResult);

        if (!correctOtp)
        {
            throw new OtpInvalidException();
        }

        var userExist = await _userRepository.GetUserByPhoneAsync(phone);
        if (userExist is not null)
        {
            var token = _jwtService.GenerateToken(userExist.Id.ToString(), userExist.Phone);
            _cookieService.SetCookie("Access-cookie",token);
            return new VerifyUserDto(userExist, token);
        }
        var user = new User
        {
            Phone = phone,
            Username = phone,
            Roles = isSeller ? new List<string> { "USER", "SELLER" } : new List<string> { "USER" }
        };
        await _userRepository.AddUserAsync(user);
        var newToken = _jwtService.GenerateToken(user.Id.ToString(), user.Phone);
        _cookieService.SetCookie("Access-cookie",newToken);
        return new VerifyUserDto(user, newToken);
    }

    public async Task<GetMeDto> GetMe(ClaimsPrincipal claimsPrincipal)
    {
        var id = claimsPrincipal.FindFirstValue("userId");
        var user = await _userRepository.GetUserByIdAsync(id);
        return new GetMeDto(user);
    }
}