using Irt.Core.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Irt.Application.Helpers;

public interface IRepositoryFactory
{
    IRepository<T> CreateFactory<T>() where T : class;
}

public class RepositoryFactory(IServiceProvider serviceProvider, IOptions<DatabaseSettings> databaseSettings) : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly DatabaseSettings _databaseSettings = databaseSettings?.Value ?? throw new ArgumentNullException(nameof(databaseSettings));
    public IRepository<T> CreateFactory<T>() where T : class
    {
        // Use configuration logic to determine which repository to return
        var databaseType = _databaseSettings.DefaultDatabaseType;
        return databaseType switch
        {
            "Couchbase" => _serviceProvider.GetRequiredService<CouchbaseRepository<T>>(),
            "Postgres" => _serviceProvider.GetRequiredService<IGenericRepository<T>>(),
            //"MySql" => _serviceProvider.GetRequiredService<MySqlRepository<T>>(),
            _ => throw new ArgumentException("Invalid database type")
        };
    }
}