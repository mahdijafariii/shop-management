using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Repositories.SellerRequestRepository;
using online_shop.Services;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class SellerRequestController : ControllerBase
{
    private readonly ISellerRequestService _sellerRequestService;

    public SellerRequestController(ISellerRequestService sellerRequestService)
    {
        _sellerRequestService = sellerRequestService;
    }
    
    [HttpPost("add-seller-request")]
    public async Task<IActionResult> AddSellerRequestAsync([FromBody] CreateSellerRequest request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _sellerRequestService.CreateRequest(userId,request);
        return Ok(new
        {
            Massage = "request added successfully",
            id = result
        });
    }
    [HttpDelete("delete-seller-request")]
    public async Task<IActionResult> DeleteSellerRequestAsync([FromQuery] string requestId)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        await _sellerRequestService.DeleteSellerRequest(requestId,userId);
        return Ok(new
        {
            Massage = "request added successfully",
        });
    }   
    
    [HttpGet("get-all-seller-request")]
    public async Task<IActionResult> GetAllSellerRequestAsync([FromQuery] PaginationInputDto request , string? status = "Pending")
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _sellerRequestService.GetAllRequest(userId,request.Page,request.Limit,status);
        return Ok(new
        {
            result.Item1,
            totalCount = result.totalCount
        });
    }
    
    [HttpPatch("update-seller-request")]
    public async Task<IActionResult> UpdateSellerRequestAsync(UpdateSellerRequestDto requestDto)
    {
        var result = await _sellerRequestService.UpdateSellerRequset(requestDto);
        return Ok(new
        {
            result.Item1,
            totalCount = result.totalCount
        });
    }
    
}