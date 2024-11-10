using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Model;
using online_shop.Repositories.SellerRepository;

namespace online_shop.Services.SellerService;

public class SellerService : ISellerService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<string> AddSellerAsync(ObjectId user,AddSellerDto addSellerDto)
    {
        Seller seller = new Seller()
        {
            Name =addSellerDto.Name,
            CityId = addSellerDto.CityId,
            ContactDetails = new ContactDetails(){Phone = addSellerDto.phone},
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            User = user,
        };
        var result = await _sellerRepository.AddSellerAsync(seller);
        return result;
    }
}