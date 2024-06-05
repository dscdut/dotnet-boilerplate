using System.Linq.Expressions;

namespace DotnetBoilerplate.Application.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> AddAsync(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
}