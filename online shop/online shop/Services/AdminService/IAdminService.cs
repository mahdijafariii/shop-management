namespace online_shop.Services;

public interface IAdminService
{
    Task BanUser(string phone);
}