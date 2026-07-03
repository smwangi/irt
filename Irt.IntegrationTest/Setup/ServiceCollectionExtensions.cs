using Microsoft.Extensions.DependencyInjection;

namespace Irt.IntegrationTest.Setup
{
    public static class ServiceCollectionExtensions
    {
        extension(IServiceCollection services)
        {
            /*public IServiceCollection AddIntegrationTest()
            {
                services.AddHttpClient();
                services.AddHostedService<CustomWebApplicationFactory<Program>>();
                return services;
            }*/
            public IServiceCollection RegisterNameValidationAndRepository<T>(string collectionName)
            {
                //services.AddScoped<INameValidationChecker<T>, NameValidationCheckerService<T>>();
                //services.AddScoped<INameRepository<T>, NameRepository<T>>(x => new NameRepository<T>(x.GetService<IMongoDatabase>()!, collectionName));
                return services;
            }
        }
    }
}
