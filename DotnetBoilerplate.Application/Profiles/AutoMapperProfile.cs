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

            // Role profile
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
