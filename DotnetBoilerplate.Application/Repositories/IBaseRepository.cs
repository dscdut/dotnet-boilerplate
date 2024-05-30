using DotnetBoilerplate.Domain.Specifications;
using System.Linq.Expressions;

namespace DotnetBoilerplate.Application.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = null,
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task AddAsync(T entity);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, Specification<T>? spec = null);

    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    Task<bool> ExistsAsync(Expression<Func<T, bool>>? predicate = null, Specification<T>? spec = null);

    Task<IReadOnlyList<T>> ToListAsync(
        Expression<Func<T, bool>>? predicate = null,
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? page = null, int? size = null);

    Task<TDto?> FirstOrDefaultAsync<TDto>(
        Expression<Func<T, bool>>? predicate = null,
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<IReadOnlyList<TDto>> ToListAsync<TDto>(
        Expression<Func<T, bool>>? predicate = null,
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? page = null, int? size = null);
}