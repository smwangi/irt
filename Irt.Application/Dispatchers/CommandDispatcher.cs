using Irt.Application.Configuration.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Dispatchers;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : CommandBase<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.HandleAsync(command, cancellationToken);
    }
}