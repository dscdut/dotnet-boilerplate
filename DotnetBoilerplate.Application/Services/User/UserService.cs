using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services.CurrentUser;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Payloads;
using DotnetBoilerplate.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Application.Services.User
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

        public async Task<UserDto?> GetMe()
        {
            return await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(_currentUserService.UserId));
        }

        public async Task<UserDto> UpdateUserByIdAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId)
                       ?? throw new UserIdNotFoundException();
            var otherUser = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(updateUserDto.Email!));
            if (otherUser != null && otherUser.Id != user.Id)
            {
                throw new EmailExistedException();
            }
            _mapper.Map(updateUserDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(user.Id));
            return userDto!;
        }
    }
}
