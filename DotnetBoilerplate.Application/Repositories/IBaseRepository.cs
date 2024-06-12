﻿using DotnetBoilerplate.Domain.Specifications;
using System.Linq.Expressions;

namespace DotnetBoilerplate.Application.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    
    Task<T?> FirstOrDefaultAsync(
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    
    Task<TDto?> FirstOrDefaultAsync<TDto>(
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task AddAsync(T entity);

    Task<int> CountAsync(Specification<T>? spec = null);

    void Update(T entity);
    
    void Delete(T entity);

    Task<bool> ExistsAsync(Specification<T>? spec = null);
    
    Task<List<T>> ToListAsync(
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? page = null, int? size = null);

    Task<List<TDto>> ToListAsync<TDto>(
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? page = null, int? size = null);
}