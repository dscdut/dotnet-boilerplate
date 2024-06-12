using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Exceptions;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services.CurrentUser;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Domain.Specifications.Users;

namespace DotnetBoilerplate.Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public AdminService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var currentUserId = _currentUserService.UserId;

            var currentUserIdSpecification = new UserIdSpecification(currentUserId);

            var roleAdminSpecification = new UserRoleIdSpecification(RoleEnum.Admin);

            if (await _userRepository.ExistsAsync(currentUserIdSpecification.And(roleAdminSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to delete information");
            }
            var targetUserIdSpecification = new UserIdSpecification(id);
            var user = await _userRepository.FirstOrDefaultAsync(targetUserIdSpecification)
                       ?? throw new UserIdNotFoundException();
            if (currentUserId != id && user.RoleId == (int)RoleEnum.Admin)
            {
                throw new AuthorizationException("The admin is not authorized to delete information of other admins");
            }
            _userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto adminUpdateUserDto)
        {
            var currentUserId = _currentUserService.UserId;

            var currentUserIdSpecification = new UserIdSpecification(currentUserId);

            var roleAdminSpecification = new UserRoleIdSpecification(RoleEnum.Admin);

            if (await _userRepository.ExistsAsync(currentUserIdSpecification.And(roleAdminSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to edit information");
            }
            var targetUserIdSpecification = new UserIdSpecification(id);
            var user = await _userRepository.FirstOrDefaultAsync(targetUserIdSpecification)
                       ?? throw new UserIdNotFoundException();
            if (currentUserId != id && user.RoleId == (int)RoleEnum.Admin)
            {
                throw new AuthorizationException("The admin is not authorized to edit information of other admins");
            }
            if (!Enum.IsDefined(typeof(RoleEnum), adminUpdateUserDto.RoleId))
            {
                throw new RoleIdNotFoundException();
            }
            var otherUser = await _userRepository.FirstOrDefaultAsync(new UserEmailSpecification(adminUpdateUserDto.Email!));
            if (otherUser != null && otherUser.Id != user.Id)
            {
                throw new EmailExistedException();
            }
            user = _mapper.Map(adminUpdateUserDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(id));
            return userDto!;
        }
    }
}
