using Irt.Application.Configuration.Commands;

namespace Irt.Application.Dispatchers;

public interface ICommandDispatcher
{
    Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : CommandBase<TResult>;
    Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>;
}