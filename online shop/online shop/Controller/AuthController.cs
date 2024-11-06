using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IBanService _banService;
    private readonly IOtpService _otpService;
    private readonly ISmsService _smsService;

    public AuthController(IBanService banService, IOtpService otpService, ISmsService smsService)
    {
        _banService = banService;
        _otpService = otpService;
        _smsService = smsService;
    }

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
}