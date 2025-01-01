using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IZarinPalService _zarinPalService;

    public CheckoutController(IZarinPalService zarinPalService)
    {
        _zarinPalService = zarinPalService;
    }

    [HttpPost("create_payment")]
    public async Task<IActionResult> CreatePayment(ShippingAddressDto shippingAddressDto)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _zarinPalService.CreatePayment(userId,shippingAddressDto);
        return Ok(result);
    }
}