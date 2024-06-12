using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DotnetBoilerplate.Application.Utils;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Payloads;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Specifications.Users;

namespace DotnetBoilerplate.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IEmailService emailService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
        }


        public async Task<TokenPayload> LoginAsync(TokenObtainPairDto tokenObtainPair)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(tokenObtainPair.Email!));
            if (user == null || !BCrypt.Net.BCrypt.Verify(tokenObtainPair.Password, user.Password))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, ErrorCodeEnum.IncorrectEmailOrPassword, "Incorrect email or password.");
            }

            var tokenPayload = JwtUtil.GenerateAccessToken(user, _configuration);
            return tokenPayload;
        }

        public async Task<UserDto> RegisterAsync(RegistrationDto registration)
        {
            if (await _userRepository.ExistsAsync(new UserEmailSpecification(registration.Email!)))
            {
                throw new EmailExistedException();
            }
            var user = _mapper.Map<Domain.Entities.User>(registration);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            user.IsActive = true;
            user.IsSuperUser = false;
            user.IsStaff = false;
            user.RoleId = (int)RoleEnum.Member;
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
    }
}
