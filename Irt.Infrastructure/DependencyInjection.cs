using Irt.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Irt.Core.SeedWork;
using Irt.Infrastructure.Domain;
using Irt.Infrastructure.Processing;
using Microsoft.Extensions.Configuration;
using Irt.Core.Datasources;
using Irt.Infrastructure.Domain.Datasources;
using Irt.Core.Datasets;
using Irt.Infrastructure.Datasets;
using Irt.Core.SharedKernel;
using Irt.Core.IndicatorDefinitions;
using Irt.Infrastructure.Domain.IndicatorDefinitions;

namespace Irt.Infrastructure;

public static class DependencyInjection
{
    

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        //string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(connectionString));
        string mongoDbConnectionString = configuration.GetConnectionString("MongoDbConnection") ?? throw new ArgumentNullException(nameof(mongoDbConnectionString));
        string mongoDBName = configuration.GetConnectionString("IrtDbName") ?? throw new ArgumentNullException(nameof(mongoDBName));
        //services.AddTransient<ISqlConnectionFactory>(x => new SqlConnectionFactory(connectionString));
        services.AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetService<IrtDbContext>()!, x.GetService<IDomainEventsDispatcher>()!));
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDatasourceRepository>(x =>
            new DatasourceRepository(
                    x.GetService<IrtDbContext>()!));
        services.AddScoped<IDatasetRepository>(x =>
            new DatasetRepository(
                    x.GetService<IrtDbContext>()!));
        services.AddScoped<IIndicatorDefinitionRepository>(x =>
            new IndicatorDefinitionRepository(
                    x.GetService<IrtDbContext>()!));

        services.AddScoped(typeof(NameUniquenessChecker<>));
        services.AddScoped(typeof(INameValidationChecker<>), typeof(NameValidationChecker<>));
        
        services.AddSingleton<IrtDbContext>(x => new IrtDbContextMongo(new MongoDbSettings(mongoDbConnectionString, mongoDBName)));

        /*services.AddDbContext<OrdersContext>(options =>
            options.UseSqlServer(connectionString));*/
        /*services.AddDbContext<IrtDbContextEf>(options =>
            options.EnableSensitiveDataLogging()
                .UseMongoDB(mongoDbConnectionString, mongoDBName));*/

        //services.AddScoped<IOrderRepository, OrderRepository>();
        //services.AddScoped<IMongoDbSettings>(x => new MongoDbSettings(mongoDbConnectionString, mongoDBName));
        
        //services.AddScoped<IrtDbContext>(x => new IrtDbContextMongo(x.GetService<IMongoDbSettings>()!));
        /*services.AddDbContext<IrtDbContextEf>(x => new IrtDbContextEf(new DbContextOptionsBuilder<IrtDbContextEf>()
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging()
            .UseMongoDB(connectionString: mongoDbConnectionString, databaseName: mongoDBName)
            .Options));*/
        return services;
    }
}