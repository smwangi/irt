using Irt.Core.SharedKernel;

namespace Irt.Infrastructure.Shared;

public class Repository<T> where T : class
{

    public Repository(string collectionName)
    {
    }

    public Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize)
    {
        return null;
    }
}