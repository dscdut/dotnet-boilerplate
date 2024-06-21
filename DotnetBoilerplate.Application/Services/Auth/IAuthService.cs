using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Payloads;

namespace DotnetBoilerplate.Application.Services.Auth;


public interface IAuthService
{
    Task<TokenPayload> LoginAsync(TokenObtainPairDto loginDto);
    Task<UserDto> RegisterAsync(RegistrationDto registerDto);
}