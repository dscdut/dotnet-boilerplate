using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBoilerplate.Infrastructure.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RolesAuthorize : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public RoleEnum[] RequiredRoles { get; set; }

        public RolesAuthorize(params RoleEnum[] roles)
        {
            RequiredRoles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //var userId = context.HttpContext.User.FindFirstValue("user_id");
            //if (string.IsNullOrEmpty(userId))
            //{
            //    throw new CustomException(StatusCodes.Status403Forbidden, "You do not have access to this resource!");
            //}

            //var userService = context.HttpContext.RequestServices.GetService<IUserService>();
            //if (userService == null)
            //{
            //    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            //    return;
            //}

            //var user = await userService.GetByIdAsync(int.Parse(userId));
            //if (user is null || !RequiredRoles.Select(r => r.ToString()).Contains(user.Role.Name))
            //{
            //    throw new CustomException(StatusCodes.Status403Forbidden, "You do not have access to this resource!");
            //}
        }
    }

}
