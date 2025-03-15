using Couchbase;
using Couchbase.KeyValue;
using Microsoft.Extensions.Options;

namespace Irt.Application.Helpers;

public interface ICouchbaseCollectionProvider
{
    ICouchbaseCollection GetCollection<T>() where T : class;
}

/**
 * Provides mappings between entities and their corresponding Couchbase collections.
 */
public class CouchbaseCollectionProvider(IBucket bucket, IOptions<DatabaseSettings> options) : ICouchbaseCollectionProvider
{
    private readonly string _collectionScope = options.Value.Scope;
    private readonly IBucket _bucket = bucket ?? throw new ArgumentNullException(nameof(bucket));
    /*private readonly ICluster _cluster;
    private readonly Dictionary<Type, (string bucket, string scope, string collection)> _collectionMappings;

    public CouchbaseCollectionProvider(ICluster cluster, Dictionary<Type, (string bucket, string scope, string collection)> collectionMappings)
    {
        _cluster = cluster;
        _collectionMappings = collectionMappings;
    }*/
    public ICouchbaseCollection GetCollection<T>() where T : class
    {
        /*var type = typeof(T);
        if (!_collectionMappings.TryGetValue(type, out var mapping))
        {
            throw new InvalidOperationException($"No collection mapping found for entity type '{type.Name}'.");
        }*/

        var collectionName = typeof(T).Name.ToLowerInvariant(); 
        /*var (bucket, scope, collection) = _collectionMappings[type];
        var bucketInstance = _cluster.BucketAsync(bucket).Result;
        var scopeInstance = bucketInstance.ScopeAsync(scope).Result;
        return scopeInstance.CollectionAsync(collection).Result;*/
        //var bucket = _cluster.BucketAsync(mapping.bucket);
        var scope = this._collectionScope; //bucket.Result.ScopeAsync(mapping.scope);
        //return scope.Result.Collection(colletionName);
        return _bucket.Scope(scope).Collection(collectionName);
    }
}