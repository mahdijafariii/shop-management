using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IBanService _banService;
    private readonly IAuthService _authService;
    private readonly ISmsService _smsService;
    private readonly IOtpService _otpService;

    public AuthController(IBanService banService, IAuthService authService, ISmsService smsService, IOtpService otpService)
    {
        _banService = banService;
        _authService = authService;
        _smsService = smsService;
        _otpService = otpService;
    }
    [AllowAnonymous]
    [HttpPost("send")]
    public async Task<IActionResult> SendOtpAsync([FromBody] OtpRequestDto request)
    {
        var phone = request.Phone;

        if (await _banService.IsBannedAsync(phone))
        {
            return StatusCode(403, new { message = "This phone number has been banned" });
        }

        var otpDetails = await _otpService.GetOtpDetailsAsync(phone);

        if (!otpDetails.Expired)
        {
            return Ok(new { message = $"OTP already sent, Please try again after {otpDetails.RemainingTime}" });
        }

        var otp = await _otpService.GenerateOtp(phone);
        _smsService.SendOtpSmsAsync(phone, otp);
        return Ok(new { message = "OTP sent successfully :))" });
    }
    [AllowAnonymous]
    [HttpPost("verify")]

    public async Task<IActionResult> Verify([FromBody] OtpVerificationRequestDto request)
    {
        var result =await _authService.VerifyOtpAndAuthUser(request.Phone, request.Otp, request.IsSeller);
        return Ok(result);
    }
    
    [HttpGet("get-me")]
    public async Task<IActionResult> GetMe()
    {
        var user = User;
        var result =await _authService.GetMe(user);
        return Ok(result);
    }
    
    
}