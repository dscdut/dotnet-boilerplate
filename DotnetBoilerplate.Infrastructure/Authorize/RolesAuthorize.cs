using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Services;
using System.Security.Claims;

namespace DotnetBoilerplate.Infrastructure.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RolesAuthorize : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly IUserService? _userService;
        public RoleEnum[] RequiredRoles { get; set; }

        public RolesAuthorize(params RoleEnum[] roles)
        {
            RequiredRoles = roles;
        }

        public RolesAuthorize(IUserService userService, params RoleEnum[] roles)
        {
            RequiredRoles = roles;
            _userService = userService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue("user_id");
            if (string.IsNullOrEmpty(userId))
            {
                throw new CustomException(StatusCodes.Status403Forbidden, "You do not have access to this resource!");
            }

            if (_userService is null)
            {
                throw new CustomException(StatusCodes.Status500InternalServerError, "User service is not provided!");
            }

            var user = await _userService.GetByIdAsync(int.Parse(userId));
            if (user is null || !RequiredRoles.Select(r => r.ToString()).Contains(user.Role.Name))
            {
                throw new CustomException(StatusCodes.Status403Forbidden, "You do not have access to this resource!");
            }
        }
    }

}
