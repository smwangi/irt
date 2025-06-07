using System.Linq.Expressions;
using Irt.Infrastructure.Shared;
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
            .ToResult(
                IrtError.NotFound($"{typeof(T).Name}  Not Found."),
                includeCountMetadata: true);
    }
    
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Set<T>()
            .AddAsync(entity, cancellationToken);
        await SaveChangesAsync();
        return entity;
    }
    
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context
            .Set<T>()
            .Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _context
            .Set<T>()
            .Remove(entity);
        return await _context
            .SaveChangesAsync() > 0;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
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
            .ToListAsync(cancellationToken)
            .ToResult(IrtError.NotFound("No items found matching the filter."), includeCountMetadata: true);
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

    public Task<Result<T>> FindByIdAsync<TKey>(TKey id)
    {
        return  _context
            .Set<T>()
            .FindByIdAsync(id: id);
    }
}