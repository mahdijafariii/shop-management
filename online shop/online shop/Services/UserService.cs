using MongoDB.Bson;
using online_shop.DTO;
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

    public async Task AddAddressAsync(AddressDto addressDto, ObjectId id)
    {
        Address address = new Address()
        {
            AddressLine = addressDto.AddressLine, Lat = addressDto.Lat, Lng = addressDto.Lng, Name = addressDto.Name,
            CityId = addressDto.CityId, PostalCode = addressDto.PostalCode
        };
        await _userRepository.AddUserAddressAsync(address,id);
    }
}