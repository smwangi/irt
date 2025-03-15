using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Dispatchers;

public static class DispatcherExtensions
{
    public static IServiceCollection AddDispatchers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
        serviceCollection.AddScoped<IQueryDispatcher, QueryDispatcher>();
        return serviceCollection;
    }
}