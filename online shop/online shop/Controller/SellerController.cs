using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Repositories.SellerRepository;
using online_shop.Services.SellerService;

namespace online_shop.Controller;

[ApiController]
[Route("api/[controller]")]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;

    public SellerController(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }


    [HttpPost("add-seller")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> AddSellerAsync([FromBody] AddSellerDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        var sellerId = await _sellerService.AddSellerAsync(objId,request);
        return Ok(new
        {
            Message = "Seller added successfully!",
            SellerId = sellerId
        });
    }
    
    [HttpPatch("update-seller")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> UpdateSellerAsync([FromQuery] string sellerId,[FromBody] AddSellerDto request)
    {
        var objId = ObjectId.Parse(sellerId);
        await _sellerService.UpdateSellerAsync(objId,request);
        return Ok(new
        {
            Message = "Seller Updated successfully!",
        });
    }
    [HttpDelete("delete-seller")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> DeleteSellerAsync([FromQuery] string sellerId)
    {
        var objId = ObjectId.Parse(sellerId);
        await _sellerService.DeleteSellerAsync(objId);
        return Ok(new
        {
            Message = "Seller deleted successfully!",
        });
        
        // ! delete products and users cart
    }
    
    [HttpGet("get-seller")]
    [Authorize(Roles = "SELLER")]
    public async Task<IActionResult> GetSellerAsync([FromQuery] string sellerId)
    {
        var objId = ObjectId.Parse(sellerId);
        var result = await _sellerService.GetSellerAsync(objId);
        return Ok(result);
    }
}