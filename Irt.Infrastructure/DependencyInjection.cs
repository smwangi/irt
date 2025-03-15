using Couchbase;
using Irt.Application.Configuration.DomainEvents;
using Irt.Application.Dispatchers;
using Irt.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database.Postgres;
using Irt.Infrastructure.Domain.Datasets;
using Microsoft.EntityFrameworkCore;
using DomainEvents_DomainEventsDispatcher = Irt.Application.Configuration.DomainEvents.DomainEventsDispatcher;


namespace Irt.Infrastructure;

public static class DependencyInjection
{
    

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cluster = Cluster.ConnectAsync("couchbase://localhost", "samwan", "P@55w.rd");
        
        services.AddSingleton<ICluster>(cluster.Result);
        
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

        services.AddScoped(typeof(IEntityRepository), typeof(EntityRepository));
        services.AddScoped<IDomainEventInterface.IDomainEventDispatcher, DomainEvents_DomainEventsDispatcher>();
        services.AddScoped<IDomainEventInterface.IDomainEventHandler<EntityCreatedEvent>, EntityCreatedEventHandler>();
        services.AddScoped<IDomainEventInterface.IDomainEventHandler<EntityModifiedEvent>, EntityModifiedEventHandler>();
        
        return services;
    }
}