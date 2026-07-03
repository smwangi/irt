using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class GenericRepository<T>(
    ApplicationDbContext applicationDbContext) 
    : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    
    public async Task<Result<List<T>>> GetAllAsync()
    {
        return await _context
            .Set<T>()
            .ToListAsync()
            /*.ToResult(
                IrtError.NotFound($"{typeof(T).Name}  Not Found."),
                includeCountMetadata: true)*/;
    }
    
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Set<T>()
            .AddAsync(entity, cancellationToken);
        return entity;
    }
    
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context
            .Set<T>()
            .Update(entity);
        return entity;
    }
    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _context
            .Set<T>()
            .Remove(entity);
        await Task.CompletedTask;
        return true;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<bool>> ExistsAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<T>()
            .AnyAsync(predicate, cancellationToken);
        return Result<bool>.Success(exists);
    }
    
    public async Task<Result<List<T>>> FilterAsync(
        Expression<Func<T, bool>> predicate, 
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<T>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
        //.ToResult(IrtError.NotFound("No items found matching the filter."), includeCountMetadata: true);
    }
    public async Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize)
    {
        var totalCount = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<T>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<T>()
            .FindAsync([CoerceKey(id, GetIdPropertyType())], cancellationToken);
            //.ToResult(IrtError.NotFound($"Entity Not Found: {typeof(TKey).Name}"));
    }

    public IQueryable<T> Query()
    {
        var query = _context.Set<T>().AsQueryable();

        // Soft delete filter in infra
        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
        }

        return query.AsNoTracking();
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TDto> Query<TDto>(IMapper mapper)
    {
        return Query().ProjectTo<TDto>(mapper.ConfigurationProvider);
    }

    private static Type GetIdPropertyType()
        => typeof(T).GetProperty("Id")?.PropertyType
           ?? throw new InvalidOperationException($"{typeof(T).Name} does not expose an Id property.");

    private static object? CoerceKey<TKey>(TKey id, Type keyType)
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
