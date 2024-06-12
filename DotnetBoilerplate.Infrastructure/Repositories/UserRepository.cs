using AutoMapper;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Entities;
namespace DotnetBoilerplate.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}