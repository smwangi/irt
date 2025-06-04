

using System.Linq.Expressions;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize);
        Task<Result<List<T>>> GetAllAsync();
        Task<Result<List<T>>> FilterByWhereClauseAsync(string query, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        
        Task<Result<bool>> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<Result<T>> FindByIdAsync<TKey>(TKey id);
        Task<Result<T>> FilterByIdAsync(string id);
    }

    public class PaginationResult<TR>
    {
        public List<TR> Items { get; set; } = [];
        public long TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}