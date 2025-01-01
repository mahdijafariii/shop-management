using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services.OrderService;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("get-user-orders")]
    public async Task<IActionResult> GetUserOrdersAsync([FromQuery] PaginationInputDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _orderService.GetUserOrders(userId,request.Page,request.Limit);
        return Ok(new
        {
            result.Item1,
            total_count = result.Item2,
            page = request.Page,
            limit = request.Limit
        });
    }   
    
    [HttpGet("get-all-orders")]
    public async Task<IActionResult> GetAllOrdersAsync([FromQuery] PaginationInputDto request)
    {
        var result = await _orderService.GetAllOrders(request.Page,request.Limit);
        return Ok(new
        {
            result.Item1,
            total_count = result.Item2,
            page = request.Page,
            limit = request.Limit
        });
    } 
}