namespace Irt.Infrastructure.Database
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }

    public class MongoDbSettings(string connectionString, string databaseName) : IMongoDbSettings
    {
        public string ConnectionString { get; } = connectionString;
        public string DatabaseName { get; } = databaseName;
    }
}