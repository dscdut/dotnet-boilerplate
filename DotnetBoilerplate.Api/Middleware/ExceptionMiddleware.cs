using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Domain.Enums;
using System.Text.Json;
using FluentValidation;

namespace DotnetBoilerplate.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                var response = new { error_code = ex.ErrorCode, message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new
                {
                    error_code = ErrorCodeEnum.InvalidSyntax,
                    message = "Invalid syntax",
                    details = ex.Errors.Select(e => e.ErrorMessage)
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new { error_code = ErrorCodeEnum.ServerError, message = "An unexpected error occurred on the server" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
