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


    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("ban-user")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> BanUserAsync([FromBody] BanUserDto request)
    {
        await _adminService.BanUser(request.Phone);
        return Ok("User banned successfully !");
    }
    
}