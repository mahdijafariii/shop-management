using MongoDB.Bson;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;

namespace online_shop.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AddAddressAsync(AddAddressDto addAddressDto, ObjectId id)
    {
        Address address = new Address()
        {
            Id = ObjectId.GenerateNewId(),AddressLine = addAddressDto.AddressLine, Lat = addAddressDto.Lat, Lng = addAddressDto.Lng, Name = addAddressDto.Name,
            CityId = addAddressDto.CityId, PostalCode = addAddressDto.PostalCode
        };
        await _userRepository.AddUserAddressAsync(address,id);
    }
    
    public async Task DeleteAddressAddressAsync(ObjectId addressId, ObjectId userId)
    { 
        var result = await _userRepository.DeleteUserAddressAsync(userId,addressId);
        if (!result)
        {
            throw new DeleteAddressException();
        }
    }
}