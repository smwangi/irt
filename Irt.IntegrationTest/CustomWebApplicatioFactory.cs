using Irt.Application.Behaviors;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Infrastructure.Database;
using Irt.Infrastructure.Datasets;
using Irt.Infrastructure.Domain.Datasources;
using Irt.Infrastructure.Domain.IndicatorDefinitions;
using Irt.Infrastructure.Shared;
using Irt.IntegrationTest.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.IntegrationTest
{
    public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
    {
        public HttpClient HttpClient { get; private set; } = null!;

        public void Intialize()
        {
            HttpClient = CreateClient();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", "Data Source=irt.db");
            string dbConnection = Environment.GetEnvironmentVariable("MongoDbConnection") ?? "mongodb://localhost:27017";
            string db = Environment.GetEnvironmentVariable("IrtDbName") ?? "irtDb";

            builder.ConfigureAppConfiguration((context, config) =>
            {
                //config.AddJsonFile("appsettings.json");
            });
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddSingleton<IrtDbContext>(x => new IrtDbContextMongo(new MongoDbSettings(dbConnection, db)));
                services.AddScoped<IDatasourceRepository>(x => new DatasourceRepository(x.GetService<IrtDbContext>()!));
                services.AddScoped<IDatasetRepository>(x => new DatasetRepository(x.GetService<IrtDbContext>()!));
                services.AddScoped<IIndicatorDefinitionRepository>(x => new IndicatorDefinitionRepository(x.GetService<IrtDbContext>()!));
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                    cfg.AddOpenBehavior(typeof(Application.Behaviors.ValidationBehavior<,>));
                });
                services.RegisterNameValidationAndRepository<Datasource>(CollectionNames.Datasources);
                services.RegisterNameValidationAndRepository<Dataset>(CollectionNames.Datasets);
                services.RegisterNameValidationAndRepository<IndicatorDefinition>(CollectionNames.IndicatorDefinitions);
                //services.AddDbContext<ApplicationD>((_, option) => option.)
            });
        }
    }
}