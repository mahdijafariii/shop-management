using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Middleware;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;


    public AdminController(IAdminService adminService, IUserService userService)
    {
        _adminService = adminService;
        _userService = userService;
    }

    [HttpPost("ban-user")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> BanUserAsync([FromBody] BanUserDto request)
    {
        await _adminService.BanUser(request.Phone);
        return Ok("User banned successfully !");
    }
    
    [HttpGet("get-all-users")]
    [Authorize]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] PaginationInputDto request)
    {
        var result = await _userService.GetAllUsersAsync(request.Page,request.Limit);
        return Ok(new { Users = result.Users, result.Item2});
    }
    
    
}