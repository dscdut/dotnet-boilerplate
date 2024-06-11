using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Specifications.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Infrastructure.Authorization
{
    public class MyAdminInfoRequirement : IAuthorizationRequirement
    {
        public string ErrorMessage { get; }

        public MyAdminInfoRequirement(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class MyAdminInfoHandler : AuthorizationHandler<MyAdminInfoRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public MyAdminInfoHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAdminInfoRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
                throw new AuthorizationException(requirement.ErrorMessage);
            
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                // HttpContext is not available
                return;
            }

            // Retrieve the resource ID from route data
            var idString = httpContext.Request.RouteValues["id"]?.ToString();
            if (!int.TryParse(idString, out var otherId))
            {
                // Invalid or missing resource ID in route data
                return;
            }

            var userId = _currentUserService.UserId;
            var userIdSpecification = new UserIdSpecification(otherId);
            var userRoleIdSpecification = new UserRoleIdSpecification(RoleEnum.Admin);
            var specification = userIdSpecification.And(userRoleIdSpecification);
            var isOtherAdmin = await _userRepository.ExistsAsync(specification);
            if (userId != otherId && isOtherAdmin) throw new AuthorizationException(requirement.ErrorMessage);
            context.Succeed(requirement);
        }
    }
}
