using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User profile
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegistrationDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AdminUpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Role profile
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
