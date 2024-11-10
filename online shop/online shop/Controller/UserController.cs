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
    public async Task<IActionResult> BanUserAsync([FromBody] AddAddressDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        await _userService.AddAddressAsync(request,objId);
        return Ok("Address added successfully!");
    }
    
    [HttpDelete("delete-address")]
    [Authorize]
    public async Task<IActionResult> DeleteAddressAsync([FromBody] DeleteAddressDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        await _userService.DeleteAddressAsync(ObjectId.Parse(request.Id),objId);
        return Ok("Address Deleted successfully!");
    }
    
    [HttpPatch("update-address")]
    [Authorize]
    public async Task<IActionResult> UpdateAddressAsync([FromBody] UpdateAddressDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        await _userService.UpdateAddressAsync(ObjectId.Parse(request.AddressId),objId,request);
        return Ok("Address Updated successfully!");
    }
    
}