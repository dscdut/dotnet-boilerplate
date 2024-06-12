using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Api.Params;
using DotnetBoilerplate.Application.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list of users.
        /// </summary>
        /// <param name="page_size">The number of users per page.</param>
        /// <param name="page">The page number.</param>
        /// <returns>Returns a paginated list of users.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers(PaginationParams paginationParams)
        {
            var users = await _userService.GetPaginationUserAsync(paginationParams.Page, paginationParams.PageSize);
            return Ok(users);
        }
        /// <summary>
        /// Update a user with the specified user ID from JWT.
        /// </summary>
        /// <param name="data">The updated user data.</param>
        /// <returns>Returns the updated user.</returns>
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var userDto = _mapper.Map<AdminUpdateUserDto>(updateUserDto);
            var updatedUser = await _userService.UpdateUserByIdAsync(_currentUserService.UserId, userDto);
            return Ok(updatedUser);
        }
    }
}
