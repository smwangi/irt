using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database;
using MongoDB.Driver;

namespace Irt.Infrastructure.Shared;

public class Repository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public Repository(IrtDbContext database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<PaginationResult<T>> GetPaginatedAsync(FilterDefinition<T> filter, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;

        // Query the documents
        var items = await _collection
            .Find(filter)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();

        // Query the total count
        var totalCount = await _collection.CountDocumentsAsync(filter);

        // Build the result
        return new PaginationResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            CurrentPage = page,
            PageSize = pageSize
        };
    }
}