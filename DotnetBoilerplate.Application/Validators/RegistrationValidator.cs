using FluentValidation;
using DotnetBoilerplate.Application.Dtos;

namespace DotnetBoilerplate.Application.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationValidator() {
            RuleFor(x => x.Email)
            .Matches(@"^.+@.+\.(com|net|org)$")
            .WithMessage("The email address must contain at least one character before and after the '@' symbol, and end with an extension like .com, .net, .org.");

            RuleFor(x => x.Password)
                .Matches(@"^(?=.*\d)(?=.*[A-Z]).{7,50}$")
                .WithMessage("The length of the string must be between 7 and 50 characters. It must contain at least one digit and one uppercase letter.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("The password and confirmation password do not match.");

            RuleFor(x => x.FullName)
                .Matches(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z])*$")
                .WithMessage("Starts with one or more letters, allows for characters like spaces, hyphens, apostrophes, commas, and periods within the name, and does not allow trailing spaces.");
        }
    }
}
