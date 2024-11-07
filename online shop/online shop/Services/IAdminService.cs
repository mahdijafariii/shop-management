namespace online_shop.Services;

public interface IAdminService
{
    Task<bool> BanUser(string phone);
}