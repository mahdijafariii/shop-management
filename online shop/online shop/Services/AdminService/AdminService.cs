using online_shop.Repositories;

namespace online_shop.Services;

public class AdminService : IAdminService
{
    private readonly IBanUsersRepository _banUsersRepository;

    public AdminService(IBanUsersRepository banUsersRepository)
    {
        _banUsersRepository = banUsersRepository;
    }

    public async Task BanUser(string phone)
    {
        await _banUsersRepository.AddToBanedUsersAsync(phone);
    }
}