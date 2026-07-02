using Couchbase.Core.Retry;
using Irt.SharedKernel.Common;

namespace Irt.Application.Messaging;

/// <summary>
/// Marker for any request that produces a TResponse (command or query)
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IRequest<out TResponse>
{
}

/// <summary>
/// Handler for a single request type asynchronously
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Wraps every handler invocation. Behaviors run in registration order
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);
}

/// <summary>
/// Delegate to invoke the next behavior or the terminal handler
/// Instead of Func<Task<TResponse>> — same shape, but the named delegate makes stack traces and IDE tooltips infinitely more readable.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

/// <summary>
/// Single entry point controller depends on
/// </summary>
public interface ISender
{
    Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken);
}
