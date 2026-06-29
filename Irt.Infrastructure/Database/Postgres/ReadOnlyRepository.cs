using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Core.SeedWork;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class ReadOnlyRepository<T>(
    ApplicationDbContext dbContext) : IReadOnlyRepository<T> where T : class
{
    private DbSet<T> dbSet =  dbContext.Set<T>();
    
    Task<bool> IReadOnlyRepository<T>.ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => dbSet.CountAsync(cancellationToken);

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => dbSet.CountAsync(predicate, cancellationToken);

    public Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await CountAsync(cancellationToken);
        var items = await dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            TotalCount = totalCount,
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await CountAsync(predicate, cancellationToken);
        var items = await dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            TotalCount = totalCount,
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public Task<PagedResult<TDto>> GetPagedAsync<TDto>(int pageNumber, int pageSize, IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<TDto>> GetPagedAsync<TDto>(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, IMapper mapper,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> orderBy, bool ascending = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TDto>> GetOrderedAsync<TDto, TKey>(Expression<Func<T, TKey>> orderBy, IMapper mapper, bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TDto?> SingleOrDefaultAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<double> AverageAsync(Expression<Func<T, decimal>> selector, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> QueryWithIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }

    public IQueryable<T> QueryWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet.Where(predicate);
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }

    public IQueryable<TDto> QueryWithIncludes<TDto>(IMapper mapper, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query.ProjectTo<TDto>(mapper.ConfigurationProvider);
    }

    public Task<T?> FindWithIncludesAsync<TId>(TId id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Query(ISpecification<T> specification)
    {
        var query = dbSet.AsQueryable();
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
        
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if(specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        
        return query;
    }

    public IQueryable<TDto> Query<TDto>(ISpecification<T> specification, IMapper mapper)
        => Query(specification).ProjectTo<TDto>(mapper.ConfigurationProvider);

    public Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<T>> GetPagedAsync(ISpecification<T> specification, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Query() => dbSet.AsQueryable();

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        => dbSet.AsQueryable().Where(predicate);

    public IQueryable<TDto> Query<TDto>(Expression<Func<T, TDto>> projection)
        => dbSet.Select(projection);

    public IQueryable<TDto> Query<TDto>(Expression<Func<T, bool>> predicate, Expression<Func<T, TDto>> projection)
        => dbSet.Where(predicate).Select(projection);

    public IQueryable<T> QueryById<TId>(TId id)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var idProperty = Expression.Property(parameter, "Id");
        var key = CoerceKey(id, idProperty.Type);
        var constant = Expression.Constant(key, idProperty.Type);
        var equality = Expression.Equal(idProperty, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
        
        return dbSet.Where(lambda);
    }

    public IQueryable<TDto> QueryById<TDto, TId>(TId id, Expression<Func<T, TDto>> projection)
        => QueryById(id).Select(projection);

    public virtual async Task<T?> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        => await dbSet.FindAsync([CoerceKey(id, GetIdPropertyType())], cancellationToken);

    public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public Task<TDto?> FindAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TDto>> GetAllAsync<TDto>(IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private static Type GetIdPropertyType()
        => typeof(T).GetProperty("Id")?.PropertyType
           ?? throw new InvalidOperationException($"{typeof(T).Name} does not expose an Id property.");

    private static object? CoerceKey<TId>(TId id, Type keyType)
    {
        if (id is null || keyType.IsInstanceOfType(id))
        {
            return id;
        }

        if (id is string stringId && IsTypedId(keyType))
        {
            var createMethod = keyType.GetMethod(
                "Create",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static,
                [typeof(string)]);

            if (createMethod is not null)
            {
                return createMethod.Invoke(null, [stringId]);
            }
        }

        return Convert.ChangeType(id, keyType);
    }

    private static bool IsTypedId(Type type)
        => type.BaseType?.IsGenericType == true
           && type.BaseType.GetGenericTypeDefinition() == typeof(TypedIdValueBase<>);
}
