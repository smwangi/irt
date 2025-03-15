namespace Irt.Core.SharedKernel;

public interface IGenericRepository<T> : IRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task SaveChangesAsync();
    Task<bool> ExistsAsync(string id);
    Task<List<T>> FindByFilterAsync(string whereClause);
    
    Task<List<T>> FindByN1qlAsync(string n1qlQuery);
    Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize);
}