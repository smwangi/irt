using Irt.Core.SharedKernel;
using Irt.Infrastructure.Services;
using Irt.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Irt.IntegrationTest.Setup
{
    public static class ServiceCollectionExtensions
    {
        /*public static IServiceCollection AddIntegrationTest(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHostedService<CustomWebApplicationFactory<Program>>();
            return services;
        }*/
        public static IServiceCollection RegisterNameValidationAndRepository<T>(this IServiceCollection services, string collectionName)
        {
            services.AddScoped<INameValidationChecker<T>, NameValidationCheckerService<T>>();
            services.AddScoped<INameRepository<T>, NameRepository<T>>(x => new NameRepository<T>(x.GetService<IMongoDatabase>()!, collectionName));
            return services;
        }
    }
}