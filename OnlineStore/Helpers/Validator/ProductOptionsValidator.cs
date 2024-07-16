using FluentValidation;
using OnlineStore.Dtos;

namespace OnlineStore.Helpers.Validator
{
    public class ProductOptionsValidator : AbstractValidator<ProductOptionsForProductCreationDto>
    {
        public ProductOptionsValidator()
        {

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Quantity)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than zero");

            RuleFor(p => p.SKU)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!")
                .Matches(@"^[0-9a-zA-Z ]+$")
                .WithMessage("Numbers and letters only please.");

            RuleFor(p => p.ColorId)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!");

            RuleFor(p => p.SizeId)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!");
        }
    }
}
