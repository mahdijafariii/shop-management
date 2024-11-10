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
    public async Task<IActionResult> BanUserAsync([FromBody] AddSellerDto request)
    {
        var user = User;
        var id = user.FindFirstValue("userId");
        var objId =ObjectId.Parse(id);
        await _sellerService.AddSellerAsync(objId,request);
        return Ok("Seller added successfully!");
    }
}