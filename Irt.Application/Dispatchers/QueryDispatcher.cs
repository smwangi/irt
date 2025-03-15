using Irt.Application.Configuration.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Dispatchers;

public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    
    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>
    {
        var _handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await _handler.HandleAsync(query, cancellationToken);
    }
}