using Irt.Application.Configuration.Commands;
using Irt.Core.SeedWork;

namespace Irt.Application.Common;

/// <summary>
/// Calls SaveChangesAsync once after the inner command handler completes successfully,
/// so individual handlers don't need to call it explicitly.
/// </summary>
public sealed class UnitOfWorkCommandHandlerDecorator<TCommand, TResult>(
    ICommandHandler<TCommand, TResult> inner,
    IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var result = await inner.HandleAsync(command, cancellationToken);

        if (ShouldCommit(result))
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    private static bool ShouldCommit(TResult result)
    {
        if (result is null)
        {
            return true;
        }

        // Works for both Result and Result<T> from Irt.SharedKernel.Results,
        // and any other result type exposing a boolean IsSuccess property.
        var isSuccessProp = typeof(TResult).GetProperty("IsSuccess");
        if (isSuccessProp is null || isSuccessProp.PropertyType != typeof(bool))
        {
            return true;
        }

        return (bool)isSuccessProp.GetValue(result)!;
    }
}
