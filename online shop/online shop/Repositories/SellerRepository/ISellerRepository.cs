using online_shop.Model;

namespace online_shop.Repositories.SellerRepository;

public interface ISellerRepository
{
    Task<string> AddUserAsync(Seller seller);

}