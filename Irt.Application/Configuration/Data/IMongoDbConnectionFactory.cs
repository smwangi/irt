using MongoDB.Driver;

namespace Irt.Application.Configuration.Data
{
    public interface IMongoDBConnectionFactory
    {
        IMongoDatabase GetDatabase();
        IMongoClient GetClient();
    }
}