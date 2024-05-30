using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DotnetBoilerplate.Infrastructure.Authorize
{
    public class RolesRequirement : IAuthorizationRequirement {
        public List<RoleEnum> Roles { get; }
        public string ErrorMessage { get; }

        public RolesRequirement(List<RoleEnum> roles, string errorMessage)
        {
            Roles = roles;
            ErrorMessage = errorMessage;
        }
    }

    public class RolesHandler : AuthorizationHandler<RolesRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RolesHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var userId = _currentUserService.UserId;
                var exist = await _unitOfWork.UserRepository.ExistsAsync(user => requirement.Roles.Contains((RoleEnum)user.Id));
                if (exist == true)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            throw new AuthorizationException(requirement.ErrorMessage);
        }
    }
}
