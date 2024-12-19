using Microsoft.AspNetCore.Mvc;
using online_shop.Repositories.SellerRequestRepository;

namespace online_shop.Controller;
[ApiController]
[Route("api/[controller]")]
public class SellerRequestController : ControllerBase
{
    private readonly ISellerRequestRepository _sellerRequestRepository;

    public SellerRequestController(ISellerRequestRepository sellerRequestRepository)
    {
        _sellerRequestRepository = sellerRequestRepository;
    }
    
    
}