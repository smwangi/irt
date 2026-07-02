using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Messaging;

internal sealed class Sender(IServiceProvider serviceProvider) : ISender
{
    // Cache reflection-built wrappers per request type.
    private static readonly ConcurrentDictionary<Type, RequestHandlerWrapper> Wrappers = new();
    
    public Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wrapper = Wrappers.GetOrAdd(
            request.GetType(),
            requestType => (RequestHandlerWrapper)Activator.CreateInstance(
                typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)))!);
        
        return ((RequestHandlerWrapper<TResponse>)wrapper)
            .HandleAsync(request, serviceProvider, cancellationToken);
    }
}

internal abstract class RequestHandlerWrapper;

internal abstract class RequestHandlerWrapper<TResponse> : RequestHandlerWrapper
{
    public abstract Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken);
}

internal sealed class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
    where TRequest : IRequest<TResponse>
{
    public override Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var behaviors = serviceProvider
            .GetServices<IPipelineBehavior<TRequest, TResponse>>()
            .Reverse()
            .ToArray();
        
        RequestHandlerDelegate<TResponse> pipeline =
            () => handler.HandleAsync((TRequest)request, cancellationToken);

        foreach (var behavior in behaviors)
        {
            var next = pipeline;
            pipeline = () => behavior.HandleAsync((TRequest)request, next, cancellationToken);
        }

        return pipeline();
    }
}