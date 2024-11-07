using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Middleware;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("add-address")]
    [Authorize]
    public async Task<IActionResult> BanUserAsync([FromBody] AddressDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        await _userService.AddAddressAsync(request,objId);
        return Ok("Address added successfully!");
    }
    
}