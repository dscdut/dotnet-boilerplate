using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Api.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Services.Admin;

namespace DotnetBoilerplate.Api.Controllers
{
    /// <summary>
    /// Controller for managing admin-related operations.
    /// </summary>
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns an IActionResult indicating the result of the deletion.</returns>
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync(UserIdParam param)
        {
            await _adminService.DeleteUserByIdAsync(param.Id);
            return NoContent();
        }

        /// <summary>
        /// Updates a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="data">The updated user information.</param>
        /// <returns>Returns an IActionResult indicating the result of the update.</returns>
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserByIdAsync(UserIdParam param, [FromBody]AdminUpdateUserDto adminUpdateUserDto)
        {
            var user = await _adminService.UpdateUserByIdAsync(param.Id, adminUpdateUserDto);
            return Ok(user);
        }
    }
}
