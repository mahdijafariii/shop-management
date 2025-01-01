using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;

namespace online_shop.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _checkoutRepository;

    public CheckoutService(ICheckoutRepository checkoutRepository)
    {
        _checkoutRepository = checkoutRepository;
    }

    public async Task<Checkout> AddCheckout(Checkout checkout)
    {
        var checkoutRes = await _checkoutRepository.AddCheckoutAsync(checkout);
        return checkoutRes;
    }
    public async Task<Checkout> GetCheckout(string authority)
    {
        var checkout = await _checkoutRepository.GetCheckoutAsync(authority);
        if (checkout is null)
        {
            throw new NotFoundException("checkout");
        }
        return checkout;
    }
}