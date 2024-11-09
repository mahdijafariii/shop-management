using FluentValidation;
using online_shop.DTO;

namespace online_shop.Validator;

public class OtpRequestDtoValidator : AbstractValidator<OtpRequestDto>
{
    public OtpRequestDtoValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            ;
    }
    
}