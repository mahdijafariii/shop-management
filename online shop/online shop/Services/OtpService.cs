using System.Text;
using online_shop.DTO;
using StackExchange.Redis;

namespace online_shop.Services;

public class OtpService : IOtpService
{
    private readonly IDatabase _redis;
    
        public OtpService(IDatabase redis)
        {
            _redis = redis;
        }
    
        private string GetOtpRedisPattern(string phone)
        {
            return $"otp:{phone}";
        }
    
        public async Task<OtpDetails> GetOtpDetailsAsync(string phone)
        {
            var otp = await _redis.StringGetAsync(GetOtpRedisPattern(phone));
            if (otp.IsNullOrEmpty)
            {
                return new OtpDetails(true, "00:00");
            }
    
            var remainingTime = await _redis.KeyTimeToLiveAsync(GetOtpRedisPattern(phone));
            if (remainingTime.HasValue)
            {
                var minutes = (int)(remainingTime.Value.TotalMinutes);
                var seconds = (int)(remainingTime.Value.TotalSeconds % 60);
                var formattedTime = $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
    
                return new OtpDetails(false, formattedTime);
            }
    
            return new OtpDetails(true, "00:00");
        }
        
        public async Task<string> GenerateOtp(string phone, int length = 4, int expireTime = 2)
        {
            var random = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10)); 
            }

            string hashedOtp = BCrypt.Net.BCrypt.HashPassword(otp.ToString(), workFactor: 12);

            await _redis.StringSetAsync(GetOtpRedisPattern(phone), hashedOtp, TimeSpan.FromMinutes(expireTime));

            return otp.ToString();        
        }
        
        // public async Task<string> VerifyOtp(string phone)
        // {
        //     var otpResult = await _redis.StringGetAsync(GetOtpRedisPattern(phone));
        //     if (otpResult is null)
        //     {
        //         return 
        //         
        //     }
        //     return otp.ToString();        
        // }
        
}