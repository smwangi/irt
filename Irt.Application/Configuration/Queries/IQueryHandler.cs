
namespace Irt.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Handler that returns a deferred <see cref="IQueryable{T}"/> instead of a materialized list.
    /// Used by transports that further compose the query (e.g. GraphQL projection/filtering,
    /// OData query options). The IQueryable is executed by the transport middleware, so the
    /// underlying DbContext must remain alive for the duration of the request scope.
    /// </summary>
    public interface IQueryableQueryHandler<in TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
