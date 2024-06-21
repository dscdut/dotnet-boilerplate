
using DotnetBoilerplate.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, message)
        {
        }
    }
}
