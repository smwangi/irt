using Irt.Application.Configuration.Queries;
namespace Irt.Application.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>;
}
