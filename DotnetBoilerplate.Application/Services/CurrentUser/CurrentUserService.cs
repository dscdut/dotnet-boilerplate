﻿using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = (_httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value)
                    ?? throw new CustomException(StatusCodes.Status403Forbidden, ErrorCodeEnum.InvalidToken, "Invalid Token");
                return int.Parse(userIdClaim);
            }
        }
    }
}
