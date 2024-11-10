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
    
    public async Task DeleteAddressAsync(ObjectId addressId, ObjectId userId)
    { 
        var result = await _userRepository.DeleteUserAddressAsync(userId,addressId);
        if (!result)
        {
            throw new DeleteAddressException();
        }
    }
    
    public async Task UpdateAddressAsync(ObjectId addressId, ObjectId userId,UpdateAddressDto updateAddressDto)
    { 
        var result = await _userRepository.UpdateAddressAsync(userId,addressId,updateAddressDto);
        if (!result)
        {
            throw new UpdateAddressException();
        }
    }
    
    public async Task<(List<User> Users, PaginationDynamicDto)> GetAllUsersAsync(int page = 1, int limit = 10)
    { 
        var result = await _userRepository.GetAllUsersAsync(page,limit);
        PaginationDynamicDto paginationDynamicDto = new PaginationDynamicDto(page = 1, limit = 10 , result.TotalCount);
        return (result.Users,paginationDynamicDto);
    }
}