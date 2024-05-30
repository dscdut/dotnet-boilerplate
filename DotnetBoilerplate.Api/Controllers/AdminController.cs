using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Api.Params;
using DotnetBoilerplate.Infrastructure.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Application.Dtos;

namespace DotnetBoilerplate.Api.Controllers
{
     /// <summary>
     /// Controller for managing admin-related operations.
     /// </summary>
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns an IActionResult indicating the result of the deletion.</returns>
        [HttpDelete("users/{id}")]
        [Authorize(Policy = PolicyName.CanDeleteUserPolicy)]
        public async Task<IActionResult> DeleteUserById(UserIdParam param)
        {
            await _userService.DeleteUserByIdAsync(param.Id);
            return NoContent();
        }

        /// <summary>
        /// Updates a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="data">The updated user information.</param>
        /// <returns>Returns an IActionResult indicating the result of the update.</returns>
        [HttpPut("users/{id}")]
        [Authorize(Policy = PolicyName.CanAdminUpdateUserPolicy)]
        public async Task<IActionResult> UpdateUserById(UserIdParam userIdParam, [FromBody]AdminUpdateUserDto adminUpdateUserDto)
        {
            var user = await _userService.UpdateUserByIdAsync(userIdParam.Id, adminUpdateUserDto);
            return Ok(user);
        }


    }
}
