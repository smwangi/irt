using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
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
            
            builder.ConfigureAppConfiguration((context, config) =>
            {
                //config.AddJsonFile("appsettings.json");
            });
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                services.AddHttpClient();
                 
                services.RegisterNameValidationAndRepository<Datasource>(CollectionNames.Datasources);
                services.RegisterNameValidationAndRepository<Dataset>(CollectionNames.Datasets);
                services.RegisterNameValidationAndRepository<IndicatorDefinition>(CollectionNames.IndicatorDefinitions);
                //services.AddDbContext<ApplicationD>((_, option) => option.)
            });
        }
    }
}
