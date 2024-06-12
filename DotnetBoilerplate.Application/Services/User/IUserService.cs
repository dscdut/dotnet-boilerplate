using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Payloads;

namespace DotnetBoilerplate.Application.Services.User;

public interface IUserService
{
    Task<UserDto?> GetMe();
    Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize);
    Task<UserDto> UpdateUserByIdAsync(UpdateUserDto adminUpdateUserDto);
}