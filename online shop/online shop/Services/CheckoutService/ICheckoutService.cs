using online_shop.Model;

namespace online_shop.Services;

public interface ICheckoutService
{
    Task<Checkout> AddCheckout(Checkout checkout);
    Task<Checkout> GetCheckout(string authority);

}