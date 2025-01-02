namespace online_shop.DTO;

public record ShippingAddressDto(string PostalCode, double Lat , double Lng , string Address, int CityId);
