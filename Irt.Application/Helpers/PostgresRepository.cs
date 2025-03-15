using System.Linq.Expressions;
using Irt.Core.SharedKernel;

namespace Irt.Application.Helpers;

public class PostgresRepository<T> : IRepository<T> where T : class
{
    public Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync(string query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    public Task<T?> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FilterByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}