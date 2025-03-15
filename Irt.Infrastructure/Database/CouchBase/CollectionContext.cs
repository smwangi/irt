using Couchbase.KeyValue;
using Couchbase.Query;

namespace Irt.Infrastructure.Database.CouchBase;

public class CollectionContext<T>(ICouchbaseCollection couchbaseCollection) : ICollectionContext<T>
    where T : class
{
    private readonly ICouchbaseCollection _couchbaseCollection = couchbaseCollection ?? throw new ArgumentNullException(nameof(couchbaseCollection));
   public async Task<T?> GetByIdAsync(string id)
    {
        var result = await couchbaseCollection.GetAsync(id);
        return result.ContentAs<T>();
    }

    public async Task CreateAsync(string id, T entity)
    {
        await couchbaseCollection.InsertAsync(id, entity);
    }

    public async Task UpdateAsync(string id, T entity)
    {
        await couchbaseCollection.ReplaceAsync(id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await couchbaseCollection.RemoveAsync(id);
    }

    public async Task<List<T>>QueryAsync(string n1qlQuery, object parameters = null)
    {
        var queryOptions = new QueryOptions();

        if (parameters != null)
        {
            queryOptions.Parameter(parameters);
        }

        var result = await _couchbaseCollection.Scope.Bucket.Cluster.QueryAsync<T>(n1qlQuery, queryOptions);
        
        return result.AnyAsync().Result ? await result.ToListAsync() : [];
    }
}

public interface ICollectionContext<T> where T : class
{
    Task<T?> GetByIdAsync(string id);
    Task CreateAsync(string id, T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task<List<T>> QueryAsync(string n1qlQuery, object parameters = null);
}