using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Domain.Dtos;
using DotnetBoilerplate.Application.Services;
using DotnetBoilerplate.Api.Utils;

namespace DotnetBoilerplate.Api.Controllers
{
    /// <summary>
    /// Controller for handling authentication operations.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Handles user login.
        /// </summary>
        /// <param name="payload">The login payload.</param>
        /// <returns>Returns the authentication token payload upon successful login.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] TokenObtainPair payload)
        {
            var tokenPayload = await _authService.Login(payload);
            return Ok(tokenPayload);
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <param name="payload">The registration payload.</param>
        /// <returns>Returns created registration</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Registration payload)
        {
            var userDto = await _authService.Register(payload);
            return StatusCode(201, ResponseUtils.CreateResponse("User created successfully", new { id = userDto.Id, name = userDto.Name, email = userDto.Email, role = userDto.Role.Id }));
        }

        /// <summary>
        /// Handles refresh token.
        /// </summary>
        /// <param name="payload">The registration payload.</param>
        /// <returns>Returns new access token</returns>
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRefresh payload)
        {
            var tokenPayload = await _authService.TokenRefresh(payload);
            return Ok(tokenPayload);
        }
    }
}
