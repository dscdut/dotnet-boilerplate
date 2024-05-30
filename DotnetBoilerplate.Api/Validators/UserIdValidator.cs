using DotnetBoilerplate.Api.Params;
using FluentValidation;

namespace DotnetBoilerplate.Api.Validators
{
    public class UserIdValidator : AbstractValidator<UserIdParam>
    {
        public UserIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("The provided user ID is in an invalid format. The ID must be a number greater than 0");
        }
    }
}
