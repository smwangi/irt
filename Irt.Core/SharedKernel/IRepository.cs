

using MongoDB.Driver;

namespace Irt.Core.SharedKernel
{
    public interface IRepository<T> where T : class
    {
        Task<PaginationResult<T>> GetPaginatedAsync(FilterDefinition<T> filter, int page, int pageSize);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }

    public class PaginationResult<TR>
    {
        public List<TR> Items { get; set; } = [];
        public long TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}