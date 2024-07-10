using DotnetBoilerplate.Api.Params;
using FluentValidation;

namespace DotnetBoilerplate.Api.Validators
{
    public class UserIdValidator : AbstractValidator<UserIdParam>
    {
        public UserIdValidator()
        {
            RuleFor(x => x.Id)
                .Must(BeValidNumber)
                .WithMessage("The provided user ID is in an invalid format. The ID must be a number greater than 0");
        }

        private bool BeValidNumber(string value)
        {
            if (int.TryParse(value, out int number))
            {
                return number > 0;
            }
            return false;
        }
    }
}
