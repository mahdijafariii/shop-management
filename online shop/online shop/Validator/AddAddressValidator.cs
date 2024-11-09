using FluentValidation;
using MongoDB.Bson.IO;
using online_shop.DTO;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace online_shop.Validator;

public class AddAddressValidator : AbstractValidator<AddAddressDto>
{
    public AddAddressValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("PostalCode is required.");

        RuleFor(x => x.Lat)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Lng)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("AddressLine is required.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("CityId must be a positive integer.")
            .Must(BeAValidCityId).WithMessage("CityId does not exist.");
    }
    
    
    private bool BeAValidCityId(int cityId)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "../online shop/cities/provinces.json");
        var jsonContent = File.ReadAllText(filePath);
        var cities = JsonConvert.DeserializeObject<List<City>>(jsonContent);
        return cities.Any(city => city.id == cityId);
    }
}
public class City
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }

}