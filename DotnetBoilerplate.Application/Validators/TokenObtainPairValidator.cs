using DotnetBoilerplate.Application.Dtos;
using FluentValidation;

namespace DotnetBoilerplate.Application.Validators
{
    public class TokenObtainPairValidator : AbstractValidator<TokenObtainPair>
    {
        public TokenObtainPairValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The email and password field is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The email and password field is required");
        }
    }
}
