using online_shop.Model;

namespace online_shop.DTO;

public record VerifyUserDto(User User, string Token);