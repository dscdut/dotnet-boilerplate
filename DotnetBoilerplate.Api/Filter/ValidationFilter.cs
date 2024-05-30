using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetBoilerplate.Api.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value == null)
                {
                    continue;
                }
                var argumentType = argument.Value.GetType();
                
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationContext = new ValidationContext<object>(argument.Value);
                    var validationResult = validator.Validate(validationContext);

                    if (!validationResult.IsValid)
                    {
                        throw new ValidationException(validationResult.Errors);
                    }
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
