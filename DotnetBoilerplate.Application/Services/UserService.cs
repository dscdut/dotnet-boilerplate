using System.Linq.Expressions;
using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Payloads;
using DotnetBoilerplate.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<PaginatedResult<UserDto>> GetPaginationUserAsync(int page, int pageSize)
        {
            var userDtos = await _userRepository.ToListAsync<UserDto>(includes: [u => u.Role], orderBy: query => query.OrderBy(user => user.Id), page: page, size: pageSize);
            var userCount = await _userRepository.CountAsync();
            return new PaginatedResult<UserDto>(userCount, userDtos);
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id)
                       ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, "The user ID does not exist");
            _userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto?> GetMe()
        {
            return await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(_currentUserService.UserId));
        }

        public async Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id) 
                       ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorCodeEnum.NotFound, "The user ID does not exist");
            user.FullName = adminUpdateUserDto.FullName;
            user.Email = adminUpdateUserDto.Email;
            user.RoleId = adminUpdateUserDto.RoleId;
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(id));
            return userDto;
        }
    }
}
