namespace online_shop.DTO;

public record UpdateAddressDto(
    string AddressId,
    string Name,
    string PostalCode,
    double? Lat,
    double? Lng,
    string AddressLine,
    int? CityId
);
