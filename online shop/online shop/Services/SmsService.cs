namespace online_shop.Services;

public class SmsService : ISmsService
{
    public string SendOtpSmsAsync(string phone, string otp)
    {
        // code of your sms provider
        Console.WriteLine($"Otp code :{otp}");
        return "Message sent successfully!!";
    }
}