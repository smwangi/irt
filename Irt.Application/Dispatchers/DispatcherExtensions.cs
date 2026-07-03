using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Dispatchers;

public static class DispatcherExtensions
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddDispatchers()
        {
            serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.AddScoped<IQueryDispatcher, QueryDispatcher>();
            return serviceCollection;
        }
    }
}
