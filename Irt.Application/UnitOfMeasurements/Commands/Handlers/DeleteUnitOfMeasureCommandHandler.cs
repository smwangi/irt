using Microsoft.EntityFrameworkCore;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.UnitOfMeasurements;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands.Handlers;

internal sealed class DeleteUnitOfMeasureCommandHandler(
    IRepository<UnitOfMeasure> repository,
    IReadOnlyRepository<IndicatorDefinition> indicatorDefinitionRepository)
    : ICommandHandler<DeleteUnitOfMeasureCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(
        DeleteUnitOfMeasureCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(UnitOfMeasureId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<Unit>.Failure(
                IrtError.NotFound($"UnitOfMeasure with id {command.Id} not found."));
        }

        var isInUse = await indicatorDefinitionRepository
            .Query(indicator => !indicator.IsDeleted && indicator.UnitOfMeasure.Id == entity.Id)
            .AnyAsync(cancellationToken);

        if (isInUse)
        {
            return Result<Unit>.Failure(
                IrtError.Conflict("Unit of measure cannot be deleted while active indicator definitions use it."));
        }

        entity.MarkAsDeleted();
        await repository.UpdateAsync(entity, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
