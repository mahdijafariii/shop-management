namespace online_shop.Services;

public interface IBanService
{
    Task<bool> IsBannedAsync(string phone);
}