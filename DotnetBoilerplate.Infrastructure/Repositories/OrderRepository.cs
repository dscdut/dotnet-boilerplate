using AutoMapper;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
