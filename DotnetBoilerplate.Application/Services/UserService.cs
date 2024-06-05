using AutoMapper;
using Microsoft.AspNetCore.Http;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;

namespace DotnetBoilerplate.Application.Services
{
    public interface IUserService
    {
        Task<User?> FindUserByEmailAsync(string email);
        Task<UserDto?> GetMe();
        //Task<UserDto> GetByIdAsync(int id);
        //Task<UserDto> CreateAsync(CreateUserDto userDto);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        //public async Task<UserDto> CreateAsync(CreateUserDto userDto)
        //{
        //    if (await _userRepository.CountAsync(user => user.Email.Equals(userDto.Email)) > 0)
        //    {
        //        throw new CustomException(StatusCodes.Status400BadRequest, "Email already exists!");
        //    }

        //    User user = _mapper.Map<User>(userDto);
        //    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        //    user.Password = hashedPassword;
        //    user.IsActive = true;
        //    user.IsSuperUser = false;
        //    user.IsStaff = false;
        //    var newUser = await _userRepository.AddAsync(user);

        //    var userDtoResult = _mapper.Map<UserDto>(newUser);
        //    return userDtoResult;
        //}

        //public async Task<UserDto> GetByIdAsync(int id)
        //{
        //    var user = await _userRepository.GetByIdAsync(id);
        //    if (user == null)
        //    {
        //        throw new CustomException(StatusCodes.Status404NotFound, "Not found.");
        //    }
        //    var userDto = _mapper.Map<UserDto>(user);
        //    return userDto;
        //}

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<UserDto?> GetMe()
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);
            return _mapper.Map<UserDto?>(user);
        }
    }
}
