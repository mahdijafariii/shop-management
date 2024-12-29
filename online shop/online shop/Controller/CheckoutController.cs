using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("create_payment")]
    public async Task<IActionResult> CreatePayment()
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _zarinPalService.CreatePayment(userId);
        return Ok(result);
    }
}