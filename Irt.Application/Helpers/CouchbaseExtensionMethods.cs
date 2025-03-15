namespace Irt.Application.Helpers;

public static class CouchbaseExtensionMethods
{
    // Query by custom logic
    public static async Task<List<T>>QueyByFilterAsync<T>(
        this ICouchbaseCollectionProvider couchbaseCollectionProvider,
        string whereClause) where T : class
    {
        var collection = couchbaseCollectionProvider.GetCollection<T>();
        var query = $"SELECT * FROM `{collection.Scope.Bucket.Name}`.`{collection.Scope.Name}`.`{collection.Name}` WHERE {whereClause}";
        var result = await collection.Scope.Bucket.Cluster.QueryAsync<T>(query);
        return await result.ToListAsync();
    }
    
    // Query all documents for an entity type
    public static async Task<List<T>> GetAllAsync<T>(
        this ICouchbaseCollectionProvider couchbaseCollectionProvider,
        CancellationToken cancellationToken) where T : class
    {
        var collection = couchbaseCollectionProvider.GetCollection<T>();
        var query = $"SELECT * FROM `{collection.Scope.Bucket.Name}`.`{collection.Scope.Name}`.`{collection.Name}s`";
        var result = await collection.Scope.Bucket.Cluster.QueryAsync<T>(query);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }
    
    // Query by N1QL 
    public static async Task<List<T>> QueryByN1qlAsync<T>(
        this ICouchbaseCollectionProvider couchbaseCollectionProvider,
        string n1qlQuery,
        CancellationToken cancellationToken) where T : class
    {
        var collection = couchbaseCollectionProvider.GetCollection<T>();
        var result = await collection.Scope.Bucket.Cluster.QueryAsync<T>(n1qlQuery);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }
}