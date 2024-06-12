using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Payloads;

namespace DotnetBoilerplate.Application.Services;

public interface IUserService
{
    Task<UserDto?> GetMe();
    Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize);
    Task DeleteUserByIdAsync(int id);

    Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto);
}