using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class OtpController : ControllerBase
{
    private readonly IBanService _banService;
    private readonly IOtpService _otpService;
    private readonly ISmsService _smsService;

    public OtpController(IBanService banService, IOtpService otpService, ISmsService smsService)
    {
        _banService = banService;
        _otpService = otpService;
        _smsService = smsService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendOtpAsync([FromBody] OtpRequestDto request)
    {
        var phone = request.Phone;

        // بررسی اینکه آیا شماره تلفن در لیست ممنوعه است
        if (await _banService.IsBannedAsync(phone))
        {
            return StatusCode(403, new { message = "This phone number has been banned" });
        }

        // بررسی جزئیات OTP برای تعیین اعتبار آن
        var otpDetails = await _otpService.GetOtpDetailsAsync(phone);

        if (!otpDetails.Expired)
        {
            return Ok(new { message = $"OTP already sent, Please try again after {otpDetails.RemainingTime}" });
        }

        // تولید و ارسال OTP
        var otp = _otpService.GenerateOtp(phone);
        await _smsService.SendSmsAsync(phone, otp);

        return Ok(new { message = "OTP sent successfully :))" });
    }
}