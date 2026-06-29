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
                userDetails.UserId ?? "anonymous",
                userDetails.UserName ?? "unknown",
                userDetails.Application ?? "api",
                userDetails.IpAddress ?? "0.0.0.0");
        }

        return await innerHandler.HandleAsync(request, cancellationToken);
    }
}