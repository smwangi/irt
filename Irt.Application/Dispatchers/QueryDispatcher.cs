using Irt.Application.Configuration.Queries;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Dispatchers;

public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    
    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) 
        where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query, cancellationToken);
    }

    public async Task<TResult> DispatchODataAsync<TQuery, TResult>(
        TQuery query,
        ODataQueryOptions queryOptions,
        CancellationToken cancellationToken)
        where TQuery : IODataQuery<TResult>
    {
        query.Options = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
        var handler = _serviceProvider.GetRequiredService<IQueryableQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query, cancellationToken);
    }
}
