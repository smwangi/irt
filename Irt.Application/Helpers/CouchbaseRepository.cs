using System.Linq.Expressions;
using Irt.Core.SharedKernel;

namespace Irt.Application.Helpers;

public class CouchbaseRepository<T>(ICouchbaseCollectionProvider collectionProvider) : IRepository<T>
    where T : class
{
    private readonly ICouchbaseCollectionProvider _collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
    //private readonly ICouchbaseCollection _couchbaseCollection;

    //_couchbaseCollection = _collectionProvider.GetCollection<T>();


    public Task<PaginationResult<T>> GetPaginatedAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> GetAllAsync(string query, CancellationToken cancellationToken)
    {
        //var result = await _couchbaseCollection.Scope.Bucket.Cluster.QueryAsync<T>(query);
        //return await result.ToListAsync();
        return await _collectionProvider.GetAllAsync<T>(cancellationToken);
    }

    public async Task<List<T>> FindByFilterAsync(string whereClause)
    {
        return await _collectionProvider.QueyByFilterAsync<T>(whereClause);
    }

    public async Task<List<T>> FindByN1qlAsync(string n1qlQuery, CancellationToken cancellationToken)
    {
        return await _collectionProvider.QueryByN1qlAsync<T>(n1qlQuery, cancellationToken);
    }

    public async Task<T?> FindByIdAsync(string id)
    {
        var whereClause = $"META().id = '{id}'";
        var result = await _collectionProvider.QueyByFilterAsync<T>(whereClause);
        return result.FirstOrDefault();
    }

    public Task<T?> FilterByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        //await _couchbaseCollection.Scope.Bucket.Cluster.UpdateAsync("1", entity);
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
}