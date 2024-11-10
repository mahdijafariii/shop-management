using FluentValidation;
using Newtonsoft.Json;
using online_shop.DTO;

namespace online_shop.Validator;

public class AddSellerValidator : AbstractValidator<AddSellerDto>
{
    public AddSellerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        RuleFor(x => x.phone)
            .NotEmpty().WithMessage("Phone is required.");

        RuleFor(x => int.Parse(x.CityId))
            .GreaterThan(0).WithMessage("CityId must be a positive integer.")
            .Must(BeAValidCityId).WithMessage("CityId does not exist.");
    }


    private bool BeAValidCityId(int cityId)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "../online shop/cities/provinces.json");
        var jsonContent = File.ReadAllText(filePath);
        var cities = JsonConvert.DeserializeObject<List<Provinces>>(jsonContent);
        return cities.Any(city => city.id == cityId);
    }
}

