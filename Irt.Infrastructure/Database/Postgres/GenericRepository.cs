using System.Linq.Expressions;
using Irt.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class GenericRepository<T>(ApplicationDbContext applicationDbContext) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    
    public async Task<T?> GetByIdAsync(object id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(string query, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FromSqlRaw(query).ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id) != null;
    }
    
    public async Task<List<T>> FindByFilterAsync(string whereClause)
    {
        return await _context.Set<T>().FromSqlRaw(whereClause).ToListAsync();
    }
    
    public async Task<T?> FilterByIdAsync(string id)
    {
        return await _context.Set<T>()
            .FromSqlInterpolated($"SELECT * FROM irt.datasources WHERE \"Id\" = {id}")
            .FirstOrDefaultAsync();
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

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> FindByIdAsync(string id)
    {
        return await _context.Set<T>().FindAsync(new object[] { id });
    }

    public async Task<List<T>> QueryAsync(string n1QlQuery, object parameters = null)
    {
        var result = await _context.Set<T>().FromSqlRaw(n1QlQuery, parameters).ToListAsync();
        return result;
    }
    public async Task<List<T>> QueryAsync(string n1qlQuery)
    {
        var result = await _context.Set<T>().FromSqlRaw(n1qlQuery).ToListAsync();
        return result;
    }
    public async Task<List<T>> QueryAsync(string n1qlQuery, object parameters = null, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>().FromSqlRaw(n1qlQuery, parameters).ToListAsync(cancellationToken);
        return result;
    }
    public async Task<List<T>> QueryAsync(string n1qlQuery, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>().FromSqlRaw(n1qlQuery).ToListAsync(cancellationToken);
        return result;
    }
}