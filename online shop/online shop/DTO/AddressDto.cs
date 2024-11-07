using online_shop.Model;

namespace online_shop.DTO;

public record AddressDto(string Name,string PostalCode,double Lat,double Lng,string AddressLine,int CityId);