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
            CreateMap<CreateUserDto, Registration>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleId))
                .ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();

            // Role profile
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
