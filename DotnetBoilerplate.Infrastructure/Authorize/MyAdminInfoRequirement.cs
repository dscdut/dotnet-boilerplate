using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Infrastructure.Authorize
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
        private readonly IUnitOfWork _unitOfWork;

        public MyAdminInfoHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAdminInfoRequirement requirement)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    // HttpContext is not available
                    return;
                }

                // Retrieve the resource ID from route data
                var idString = httpContext.Request.RouteValues["id"]?.ToString();
                if (!int.TryParse(idString, out int otherId))
                {
                    // Invalid or missing resource ID in route data
                    return;
                }

                var userId = _currentUserService.UserId;
                var isOtherAdmin = await _unitOfWork.UserRepository.ExistsAsync(u => u.Id == otherId && u.RoleId == (int)RoleEnum.Admin);
                if (userId == otherId || !isOtherAdmin)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            throw new AuthorizationException(requirement.ErrorMessage);
        }
    }
}
