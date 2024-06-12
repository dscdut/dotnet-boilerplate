using Microsoft.AspNetCore.Mvc;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Services;

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
        public async Task<IActionResult> LoginAsync([FromBody] TokenObtainPairDto payload)
        {
            var tokenPayload = await _authService.LoginAsync(payload);
            return Ok(tokenPayload);
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <param name="payload">The registration payload.</param>
        /// <returns>Returns created registration</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationDto payload)
        {
            var userDto = await _authService.RegisterAsync(payload);
            return StatusCode(201, userDto);
        }
    }
}
