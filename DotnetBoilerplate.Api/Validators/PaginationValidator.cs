using FluentValidation;
using DotnetBoilerplate.Api.Params;

namespace DotnetBoilerplate.Api.Validators
{
    public class PaginationValidator: AbstractValidator<PaginationParams>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("The invalid parameter is passed to the page. The 'page' parameter must be a number greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("The invalid parameter is passed to the page_size. The 'page_size' parameter must be a number greater than 0");
        }
    }
}
