using online_shop.Model;

namespace online_shop.DTO;

public record AddAddressDto(string Name,string PostalCode,double Lat,double Lng,string AddressLine,int CityId);