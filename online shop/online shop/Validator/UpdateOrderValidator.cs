using FluentValidation;
using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Validator;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(x => x.Status)
            .Must(status => 
                status == OrderStatus.SHIPPED.ToString() || 
                status == OrderStatus.DELIVERED.ToString())
            .WithMessage($"Status must be either '{OrderStatus.SHIPPED}' or '{OrderStatus.DELIVERED}'.");    
        
        RuleFor(x => x.PostTrackingCode)
            .NotEmpty().WithMessage("PostTrackingCode is required.");;
    }
    
}