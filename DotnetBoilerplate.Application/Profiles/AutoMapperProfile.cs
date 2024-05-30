using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Domain.Enums;

namespace DotnetBoilerplate.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User profile
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegistrationDto>().ReverseMap();
            CreateMap<UpdateUserDto, AdminUpdateUserDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => RoleEnum.Member));

            // Role profile
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
