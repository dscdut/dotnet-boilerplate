using DotnetBoilerplate.Application.Repositories;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using DotnetBoilerplate.Domain.Specifications;

namespace DotnetBoilerplate.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IMapper _mapper;

        public BaseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate, Specification<T>? spec)
        {
            var query = _dbSet.AsQueryable();
            return await query.ApplyFilter(predicate).ApplySpecification(spec).CountAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>>? predicate, Specification<T>? spec)
        {
            var query = _dbSet.AsQueryable();
            return await query.ApplyFilter(predicate).ApplySpecification(spec).AnyAsync();
        }

        public virtual Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate,
            Specification<T>? spec,
            List<Expression<Func<T, object>>>? includes,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            var query = _dbSet.AsQueryable();
            return await query
                .ApplyFilter(predicate)
                .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<TDto?> FirstOrDefaultAsync<TDto>(
            Expression<Func<T, bool>>? predicate,
            Specification<T>? spec,
            List<Expression<Func<T, object>>>? includes,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            var query = _dbSet.AsQueryable();
            return await query
                .ApplyFilter(predicate)
                .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IReadOnlyList<T>> ToListAsync(
            Expression<Func<T, bool>>? predicate,
            Specification<T>? spec,
            List<Expression<Func<T, object>>>? includes,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            int? page, int? size)
        {
            var query = _dbSet.AsQueryable();
            return await query
                .ApplyFilter(predicate)
                .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .ApplyPaging(page, size)
                .ToListAsync();
        }

        public virtual async Task<IReadOnlyList<TDto>> ToListAsync<TDto>(
            Expression<Func<T, bool>>? predicate,
            Specification<T>? spec,
            List<Expression<Func<T, object>>>? includes,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            int? page, int? size)
        {
            var query = _dbSet.AsQueryable();
            return await query
                .ApplyFilter(predicate)
                .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .ApplyPaging(page, size)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }

    static class RepositoryExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int? page, int? size)
        {
            if (page != null && size != null)
            {
                return query.Skip((page.Value - 1) * size.Value).Take(size.Value);
            }
            return query;
        }

        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, Specification<T>? spec)
        {
            if (spec != null)
            {
                query = query.ApplyFilter(spec.ToExpression());
            }
            return query;
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>>? predicate)
        {
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }

        public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, List<Expression<Func<T, object>>>? includes) where T : class
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
