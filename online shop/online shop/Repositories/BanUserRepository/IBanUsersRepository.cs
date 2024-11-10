namespace online_shop.Repositories;

public interface IBanUsersRepository
{
    Task<bool> IsUserBannedAsync(string phone);
    Task AddToBanedUsersAsync(string phone);
}