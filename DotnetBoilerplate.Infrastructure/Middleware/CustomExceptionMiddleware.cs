using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DotnetBoilerplate.Application.Exceptions;

namespace DotnetBoilerplate.Infrastructure.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
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
                var response = new { detail = ex.Message };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new { detail = "An unexpected error occurred." + ex.Message };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
