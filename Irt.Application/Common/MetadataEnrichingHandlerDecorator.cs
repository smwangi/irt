using Irt.Application.Configuration.Commands;

namespace Irt.Application.Common;

public class MetadataEnrichingHandlerDecorator<TCommand, TResult>(
    ICommandHandler<TCommand, TResult> innerHandler,
    IUserDetails userDetails)
    : ICommandHandler<TCommand, TResult>
    where TCommand : class, ICommand<TResult>
{
    public async Task<TResult> HandleAsync(TCommand request, CancellationToken cancellationToken)
    {
        if (request is IRequireMetadata requireMetadata)
        {
            requireMetadata.SetMetadata(
                userDetails.UserId,
                userDetails.UserName,
                userDetails.Application);
        }

        return await innerHandler.HandleAsync(request, cancellationToken);
    }
}