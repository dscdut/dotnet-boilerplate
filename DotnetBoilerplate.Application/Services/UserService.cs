using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Payloads;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetMe();
        Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize);
        Task DeleteUserByIdAsync(int id);

        Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IMapper mapper, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize)
        {
            var userDtos = await _unitOfWork.UserRepository.ToListAsync<UserDto>(
                page: page, size: pageSize, orderBy: query => query.OrderBy(user => user.Id), includes: [u => u.Role!]);
            var userCount = await _unitOfWork.UserRepository.CountAsync();
            return new PaginatedResult<UserDto>(userCount, userDtos);
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
                ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, "The user ID does not exist");
            await _unitOfWork.UserRepository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto?> GetMe()
        {
            return await _unitOfWork.UserRepository.FirstOrDefaultAsync<UserDto>(u => u.Id == _currentUserService.UserId);
        }

        public async Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id) 
                ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, "The user ID does not exist");
            user.FullName = adminUpdateUserDto.FullName;
            user.Email = adminUpdateUserDto.Email;
            user.RoleId = adminUpdateUserDto.RoleId!.Value;
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _unitOfWork.UserRepository.FirstOrDefaultAsync<UserDto>(u => u.Id == id);
            return userDto;
        }
    }
}
