using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Specifications.Users;
using Microsoft.AspNetCore.Authorization;

namespace DotnetBoilerplate.Infrastructure.Authorization
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
        private readonly IUserRepository _userRepository;


        public RolesHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
                throw new AuthorizationException(requirement.ErrorMessage);
           
            var userIdSpecification = new UserIdSpecification(_currentUserService.UserId);

            var userHasRoleSpecification = new UserHasRoleSpecification(requirement.Roles);

            var specification = userIdSpecification.And(userHasRoleSpecification);
            
            var exist = await _userRepository.ExistsAsync(specification);
            
            if (exist != true) throw new AuthorizationException(requirement.ErrorMessage);
            
            context.Succeed(requirement);
        }
    }
}
