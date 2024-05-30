using System;

namespace DotnetBoilerplate.Application.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }

        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
