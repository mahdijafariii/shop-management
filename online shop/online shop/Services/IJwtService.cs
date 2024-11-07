namespace online_shop.Services;

public interface IJwtService
{
    string GenerateToken(string userId, string phone, List<string> roles);
}