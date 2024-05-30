using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Infrastructure.Authorize;

namespace DotnetBoilerplate.Api.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>Returns the user with the specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        [RolesAuthorize(RequiredRoles = new RoleEnum[] { RoleEnum.Admin })]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Get the current user.
        /// </summary>
        /// <returns>Returns current user information.</returns>
        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            return Ok(await _userService.GetMe());
        }
    }
}
