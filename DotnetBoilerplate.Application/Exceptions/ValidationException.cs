using FluentValidation.Results;

namespace DotnetBoilerplate.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationFailure> Errors { get; }

        public ValidationException(List<ValidationFailure> failures)
        {
            Errors = failures;
        }
    }
}
