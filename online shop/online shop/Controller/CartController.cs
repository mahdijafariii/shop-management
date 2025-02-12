using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add-to-cart")]
    public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _cartService.AddToCart(request,userId);
        return Ok(new
        {
            Massage = "Added successfully",
            CartItems = result
        });
    }    
    
    [HttpDelete("delete-from-cart")]
    public async Task<IActionResult> DeleteFromCartAsync([FromBody] DeleteFromCartDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _cartService.DeleteFromCart(request,userId);
        return Ok(new
        {
            Massage = "Deleted successfully",
            CartItems = result
        });
    }    
    
    [HttpGet("get-cart")]
    public async Task<IActionResult> GetCartAsync()
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _cartService.GetCart(userId);
        return Ok(result);
    }   
}