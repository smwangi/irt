using Couchbase;
using Irt.Application.Dispatchers;
using Irt.Application.Helpers;
using Irt.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Irt.Infrastructure.Processing;
using Microsoft.Extensions.Configuration;
using Irt.Core.Datasources;
using Irt.Infrastructure.Domain.Datasources;
using Irt.Core.Datasets;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database.Postgres;
using Irt.Infrastructure.Domain.Datasets;
using Irt.Infrastructure.Domain.IndicatorDefinitions;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure;

public static class DependencyInjection
{
    

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cluster = Cluster.ConnectAsync("couchbase://localhost", "samwan", "P@55w.rd");
        
        services.AddSingleton<ICluster>(cluster.Result);
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        
        services.AddDispatchers();
        services.AddRepositories();
        //services.AddScoped<IDatasourceRepository, DatasourceRepository>();
        //services.AddScoped<IIndicatorDefinitionRepository, IndicatorDefinitionRepository>();
        
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        
        services.AddSingleton<ICluster>(sp =>
        {
            var connectionString = configuration.GetSection("Couchbase:ConnectionString").Value;
            var username = configuration.GetSection("Couchbase:Username").Value;
            var password = configuration.GetSection("Couchbase:Password").Value;

            var cluster = Cluster.ConnectAsync(connectionString, username, password).Result;
            return cluster;
        });
        services.AddSingleton<IBucket>(sp =>
        {
            var cluster = sp.GetRequiredService<ICluster>();
            var bucketName = configuration.GetSection("Couchbase:BucketName").Value;
            return cluster.BucketAsync(bucketName).Result;
        });
        services.AddSingleton<ICouchbaseCollectionProvider, CouchbaseCollectionProvider>();
        services.AddScoped(typeof(IRepository<>), typeof(CouchbaseRepository<>)); // couchbase as default
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped(typeof(CouchbaseRepository<>));
        services.AddScoped(typeof(PostgresRepository<>));
        services.AddScoped(typeof(DatasetService));
        
        return services;
    }
}