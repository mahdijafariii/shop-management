using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{


    public AdminController()
    {
        
    }

    [HttpPost("ban-user")]
    public async Task<IActionResult> SendOtpAsync([FromBody] OtpRequestDto request)
    {
        
    }
    
}