namespace online_shop.Services;

public interface ISmsService
{
    string SendOtpSmsAsync(string phone,string otp);
}