using AutoMapper;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Infrastructure.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}