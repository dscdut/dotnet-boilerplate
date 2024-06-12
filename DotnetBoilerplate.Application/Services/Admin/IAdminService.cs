using DotnetBoilerplate.Application.Dtos;

namespace DotnetBoilerplate.Application.Services.Admin
{
    public interface IAdminService
    {
        Task DeleteUserByIdAsync(int id);
        Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto);
    }
}
