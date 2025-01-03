using MongoDB.Driver;

namespace Irt.Infrastructure.Database
{
    public interface IrtDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

    public class IrtDbContextMongo : IrtDbContext
    {
        private readonly IMongoDatabase _database;
        
        public IrtDbContextMongo(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}