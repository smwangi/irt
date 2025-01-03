using Irt.Core.SharedKernel;
using MongoDB.Driver;

namespace Irt.Infrastructure.Shared
{
    public class NameRepository<TEntity>(IMongoDatabase database, string collectionName) : INameRepository<TEntity>
    {
        private readonly IMongoCollection<TEntity> _collection = database.GetCollection<TEntity>(collectionName);

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken, string? excludeEntityId = null)
        {
            var filter = Builders<TEntity>.Filter.Eq("Name.Value", name);

            if (excludeEntityId != null)
            {
                filter &= Builders<TEntity>.Filter.Ne("_id", excludeEntityId);
            }

            return await _collection.Find(filter).AnyAsync(cancellationToken);
        }
    }
}