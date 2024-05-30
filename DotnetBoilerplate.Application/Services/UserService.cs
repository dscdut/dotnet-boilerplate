using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DotnetBoilerplate.Domain;
using DotnetBoilerplate.Domain.Dtos;
using DotnetBoilerplate.Domain.Models;
using DotnetBoilerplate.Application.Exceptions;

namespace DotnetBoilerplate.Application.Services
{
    public interface IUserService
    {
        Task<User> FindUserByEmailAsync(string email);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<UserDto> GetMe();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto userDto);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public UserService(DataContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto userDto)
        {
            try
            {
                if (await UserExistsByEmailAsync(userDto.Email))
                {
                    throw new CustomException(StatusCodes.Status400BadRequest, "Email already exists!");
                }

                User user = _mapper.Map<User>(userDto);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = hashedPassword;
                user.IsActive = true;
                user.IsSuperUser = false;
                user.IsStaff = false;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                UserDto userDtoResult = _mapper.Map<UserDto>(user);
                return userDtoResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err", ex.Message);
                throw new CustomException(500, ex.Message);
            }
        }
      
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new CustomException(StatusCodes.Status404NotFound, "Not found.");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserDto> GetMe()
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
