namespace online_shop.DTO;

public record OtpVerificationRequestDto(string Phone,string Otp,bool IsSeller);