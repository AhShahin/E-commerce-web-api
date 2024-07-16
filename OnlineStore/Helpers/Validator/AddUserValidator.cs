using FluentValidation;
using FluentValidation.Validators;
using OnlineStore.Dtos;
using OnlineStore.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Helpers.Validator
{
    public class AddUserValidator : AbstractValidator<UserForCreationDto>
    {
        public AddUserValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50)
                .WithMessage("Length ({TotalLength}) of {PropertyName} Invalid!")
                .Must(IsValidName)
                .WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!")
                .Length(2, 50)
                .WithMessage("Length ({TotalLength}) of {PropertyName} Invalid!")
                .Must(IsValidName)
                .WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .IsEnumName(typeof(Gender))
                .WithMessage("Gender must be either Male or Female");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} address is required")
                .EmailAddress(EmailValidationMode.Net4xRegex);
            
            RuleFor(x => x.TelephoneCell)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Matches(new Regex(@"^(\+\d{1,2}\s)?\(?\d{3,4}\)?[\s.-]\d{3}[\s.-]\d{4}$"))
                .WithMessage("{PropertyName} not valid");

            RuleFor(x => x.TelephoneHome)
                .Matches(new Regex(@"^(\+\d{1,2}\s)?\(?\d{3,4}\)?[\s.-]\d{3}[\s.-]\d{4}$"))
                .WithMessage("{PropertyName} not valid")
                .When(x => !string.IsNullOrEmpty(x.TelephoneHome));

            RuleFor(u => u.DoB)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must(IsValidAge)
                .WithMessage("Invalid {PropertyName}, Age must be at least 18. ")
                .WithName("Date of birth");

            RuleFor(p => p.KnownAs)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!")
                .Length(2, 50)
                .WithMessage("Length ({TotalLength}) of {PropertyName} Invalid!");
            
            RuleFor(p => p.Type)
                .NotEmpty()
                .WithMessage("{PropertyName} is empty!"); 
        }

        protected bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }

        protected bool IsValidAge(DateTime date)
        {
            var currYear = DateTime.Now.Year;
            var dobYear = date.Year;
            var currDate = DateTime.Now;

            if ((DateTime.Compare(currDate.AddYears(-18), date) >= 0) && dobYear > (currYear - 120))
                return true;
            
            return false;
        }
    }
}