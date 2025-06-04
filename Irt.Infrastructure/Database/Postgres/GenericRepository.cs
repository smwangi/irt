using System.Linq.Expressions;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Shared;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class GenericRepository<T>(ApplicationDbContext applicationDbContext) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    
    public async Task<Result<List<T>>> GetAllAsync()
    {
        return await _context
            .Set<T>()
            .ToListAsync()
            .ToResult(
                Error.NotFound($"{typeof(T).Name}  Not Found."),
                includeCountMetadata: true);
    }

    public async Task<Result<List<T>>> FilterByWhereClauseAsync(string whereClause, CancellationToken cancellationToken)
    {
        return await _context
            .Set<T>()
            .FromSqlRaw(whereClause)
            .ToListAsync()
            .ToResult(
                Error.NotFound($"{typeof(T).Name}  Not Found."),
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
    
    public async Task<List<T>> FindByFilterAsync(string whereClause)
    {
        return await _context
            .Set<T>()
            .FromSqlRaw(whereClause)
            .ToListAsync();
    }
    
    public async Task<Result<T>> FilterByIdAsync(string id)
    {
        return await _context
            .Set<T>()
            .FromSqlInterpolated($"SELECT * FROM irt.datasources WHERE \"Id\" = {id}")
            .FirstOrDefaultAsync()
            .ToResult(Error.NotFound($"{typeof(T).Name}  Not Found."));
    }
    
    public async Task<List<T>> FindByN1qlAsync(string n1qlQuery)
    {
        return await _context.Set<T>().FromSqlRaw(n1qlQuery).ToListAsync();
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

    public async Task<List<T>> QueryAsync(string n1QlQuery, object parameters = null)
    {
        var result = await _context
            .Set<T>()
            .FromSqlRaw(n1QlQuery, parameters)
            .ToListAsync();
        return result;
    }
    public async Task<List<T>> QueryAsync(string n1qlQuery)
    {
        return await _context
            .Set<T>()
            .FromSqlRaw(n1qlQuery)
            .ToListAsync();
    }
    public async Task<List<T>> QueryAsync(
        string n1qlQuery,
        object parameters,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<T>()
            .FromSqlRaw(n1qlQuery, parameters)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<T>> QueryAsync(
        string n1qlQuery,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<T>()
            .FromSqlRaw(n1qlQuery)
            .ToListAsync(cancellationToken);
    }
}