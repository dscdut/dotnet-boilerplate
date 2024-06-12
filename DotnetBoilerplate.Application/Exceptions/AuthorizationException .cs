using DotnetBoilerplate.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Exceptions
{
    public class AuthorizationException : CustomException
    {
        public AuthorizationException(string message) : base(StatusCodes.Status403Forbidden, ErrorCodeEnum.NotAuthorized, message)
        {
        }
    }
}
