using Irt.Application.Configuration.Data;
using MongoDB.Driver;

namespace Irt.Infrastructure.Database
{
    public class MongoDBConnectionFactory //: IMongoDBConnectionFactory
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public MongoDBConnectionFactory(string connectionString, string databaseName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentNullException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public IMongoClient GetClient()
        {
            return new MongoClient(_connectionString);
        }

        public IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_connectionString);
            //var database = IrtDbContext.Create(client.GetDatabase(_databaseName));
            return client.GetDatabase(_connectionString+_databaseName);
        }
    }
}