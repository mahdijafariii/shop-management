using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Exception;
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
            CityId = int.Parse(addSellerDto.CityId),
            ContactDetails = new ContactDetails(){Phone = addSellerDto.phone},
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            User = user,
        };
        var checkUserSeller = await _sellerRepository.CheckUserSeller(user);
        if (checkUserSeller)
        {
            throw new AddSellerException();
        }
        var result = await _sellerRepository.AddSellerAsync(seller);
        return result;
    }
    
    public async Task UpdateSellerAsync(string sellerId ,AddSellerDto addSellerDto)
    {
        var hasSeller = await _sellerRepository.CheckStoreHasSeller(sellerId);
        if (!hasSeller)
        {
            throw new NotFoundException("Seller");
        }
        var result = await _sellerRepository.UpdateSellerAsync(sellerId,addSellerDto);
        if (!result)
        {
            throw new UpdateAddressException();
        }
    }

    public async Task DeleteSellerAsync(string seller)
    {
        var result = await _sellerRepository.DeleteSellerAsync(seller);
        if (!result)
        {
            throw new OperationFailedException();
        }
    }

    public async Task<GetSellerDto> GetSellerAsync(string seller)
    {
        var result = await _sellerRepository.GetSellerAsync(seller);
        if (!result.Item2)
        {
            throw new NotFoundException("Seller");
        }
        return result.Item1;
    }
}