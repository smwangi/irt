using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands.Handlers;

internal sealed class PatchUnitOfMeasureCommandHandler(
    IRepository<UnitOfMeasure> repository,
    INameUniquenessChecker<UnitOfMeasure, UnitOfMeasureId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<PatchUnitOfMeasureCommand, Result<UnitOfMeasureDto>>
{
    public async Task<Result<UnitOfMeasureDto>> HandleAsync(
        PatchUnitOfMeasureCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(UnitOfMeasureId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<UnitOfMeasureDto>.Failure(
                IrtError.NotFound($"UnitOfMeasure with id {command.Id} not found."));
        }

        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, entity.Id, cancellationToken))
        {
            return Result<UnitOfMeasureDto>.Failure(
                IrtError.Conflict($"A unit of measure named '{command.Name}' already exists."));
        }

        entity.Update(command.Name, command.Description);

        return Result<UnitOfMeasureDto>.Success(mapper.Map<UnitOfMeasureDto>(entity));
    }
}
