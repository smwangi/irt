

using System.Linq.Expressions;

namespace Irt.Core.SharedKernel
{
    public interface IRepository<T> where T : class
    {
        Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(string query, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindByIdAsync(string id);
        Task<T?> FilterByIdAsync(string id);
    }

    public class PaginationResult<TR>
    {
        public List<TR> Items { get; set; } = [];
        public long TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}