using online_shop.Repositories;

namespace online_shop.Services;

public class BanService : IBanService
{
    private readonly IBanUsersRepository _banUsersRepository;

    public BanService(IBanUsersRepository banUsersRepository)
    {
        _banUsersRepository = banUsersRepository;
    }
    
    public async Task<bool> IsBannedAsync(string phone)
    {
        if (await _banUsersRepository.IsUserBannedAsync(phone))
        {
            return true;
        }

        return false;
    }

    
    
}