using online_shop.Model;

namespace online_shop.Repositories;

public interface ICheckoutRepository
{
    Task<Checkout> AddCheckoutAsync(Checkout checkout);
    Task<Checkout> GetCheckoutAsync(string authority);

}