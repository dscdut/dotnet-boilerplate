using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DotnetBoilerplate.Application.Utils;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Payloads;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Validators;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Application.Services
{
    public interface IAuthService
    {
        Task<TokenPayload> Login(TokenObtainPairDto loginDto);
        Task<UserDto> Register(RegistrationDto registerDto);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
        }


        public async Task<TokenPayload> Login(TokenObtainPairDto tokenObtainPair)
        {
            var validationResult = new TokenObtainPairValidator().Validate(tokenObtainPair);
            if (!validationResult.IsValid)
            {
                throw new CustomException(StatusCodes.Status400BadRequest, ErrorCodeEnum.InvalidRequest, validationResult.Errors[0].ToString());
            }
            var user = await _userRepository.FirstOrDefaultAsync(u => tokenObtainPair.Email!.Equals(u.Email));
            if (user == null || !BCrypt.Net.BCrypt.Verify(tokenObtainPair.Password, user.Password))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, ErrorCodeEnum.IncorrectEmailOrPassword, "Incorrect email or password.");
            }

            var tokenPayload = JwtUtil.GenerateAccessToken(user, _configuration);
            return tokenPayload;
        }

        public async Task<UserDto> Register(RegistrationDto registration)
        {
            var validationResult = new RegistrationValidator().Validate(registration);
            if (!validationResult.IsValid)
            {
                throw new CustomException(StatusCodes.Status400BadRequest, ErrorCodeEnum.InvalidSyntax, "Invalid syntax");
            }
            if (await _userRepository.ExistsAsync(user => registration.Email!.Equals(user.Email)))
            {
                throw new CustomException(StatusCodes.Status409Conflict, ErrorCodeEnum.ExistedEmail, "Email already exists");
            }
            var user = _mapper.Map<User>(registration);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            user.IsActive = true;
            user.IsSuperUser = false;
            user.IsStaff = false;
            user.RoleId = 2;
            var newUser = await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(newUser) ?? throw new Exception("Error while creating user");
        }
    }
}
