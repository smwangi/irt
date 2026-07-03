using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorDefinitions;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands.Handlers;

internal sealed class DeleteIndicatorDefinitionCommandHandler(
    IRepository<IndicatorDefinition> repository)
    : ICommandHandler<DeleteIndicatorDefinitionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(
        DeleteIndicatorDefinitionCommand command, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(
            IndicatorDefinitionId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<Unit>.Failure(
                IrtError.NotFound($"IndicatorDefinition with id {command.Id} not found."));
        }

        entity.MarkAsDeleted();
        await repository.UpdateAsync(entity, cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}
