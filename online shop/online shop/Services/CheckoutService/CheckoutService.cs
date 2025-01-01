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
        var noteResult = await _checkoutRepository.AddCheckoutAsync(checkout);
        return noteResult;
    }
}