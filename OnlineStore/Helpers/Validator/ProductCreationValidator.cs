using FluentValidation.Validators;
using FluentValidation;
using OnlineStore.Dtos;
using static OnlineStore.Helpers.Enums;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace OnlineStore.Helpers.Validator
{
    public class ProductCreationValidator : AbstractValidator<ProductForCreationDto>
    {
        public ProductCreationValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50)
                .WithMessage("Length ({TotalLength}) of {PropertyName} Invalid!")
                .Must(IsValidDescription)
                .WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!")
                .Must(IsValidPrice)
                .WithMessage("{PropertyName} is not decimal type!");

            RuleFor(x => x.MaterialId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required"); ;

            RuleFor(x => x.StyleId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.SeasonId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required"); 

            RuleFor(u => u.CategoryId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleForEach(p => p.ProductOptions)
                .NotNull()
                .SetValidator(new ProductOptionsValidator());
        }

        protected bool IsValidDescription(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }

        protected bool IsValidPrice(decimal price)
        {

            return (price % 1) > 0;
        }
    }
}
