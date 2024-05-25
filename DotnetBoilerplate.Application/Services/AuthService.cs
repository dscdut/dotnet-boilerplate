using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DotnetBoilerplate.Infrastructure.Utils;
using DotnetBoilerplate.Domain.Dtos;
using DotnetBoilerplate.Domain.Payloads;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace DotnetBoilerplate.Application.Services
{
    public interface IAuthService
    {
        Task<TokenPayload> Login(TokenObtainPair loginDto);
        Task<UserDto> Register(Registration registerDto);
        Task<TokenPayload> TokenRefresh(TokenRefresh tokenRefresh);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        public AuthService(IUserService userService, IConfiguration configuration, IMapper mapper, DataContext context, IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }


        public async Task<TokenPayload> Login(TokenObtainPair tokenObtainPair)
        {
            var user = _userService.FindUserByEmailAsync(tokenObtainPair.Email);
            if (user == null)
            {
                throw new CustomException(StatusCodes.Status404NotFound, "No active account found with the given credentials");
            }
            if (user.IsActive == false)
            {
                throw new CustomException(StatusCodes.Status403Forbidden, "User is inactive!");
            }
            if (!BCrypt.Net.BCrypt.Verify(tokenObtainPair.Password, user.Password))
            {
                throw new CustomException(StatusCodes.Status404NotFound, "No active account found with the given credentials");
            }

            var tokenPayload = JwtUtil.GenerateAccessAndRefreshToken(_mapper.Map<UserDto>(user), _configuration);
            return tokenPayload;
        }

        public async Task<UserDto> Register(Registration registration)
        {
            UserDto userDto = await _userService.CreateAsync(_mapper.Map<CreateUserDto>(registration));
            return userDto;
        }

        public async Task<TokenPayload> TokenRefresh(TokenRefresh tokenRefresh)
        {
            // Parse the refreshToken string
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(tokenRefresh.Refresh);

            // Get the user_id claim from the refreshToken payload
            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "user_id");
            if (userIdClaim == null)
            {
                throw new CustomException(StatusCodes.Status400BadRequest, "Invalid refreshToken payload");
            }
            var userId = int.Parse(userIdClaim.Value);

            var user = await _userService.GetByIdAsync(userId);
            return JwtUtil.GenerateAccessToken(user, _configuration);

        }
    }
}
