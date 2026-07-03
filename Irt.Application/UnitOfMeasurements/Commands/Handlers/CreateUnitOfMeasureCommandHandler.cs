using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands.Handlers;

internal sealed class CreateUnitOfMeasureCommandHandler(
    IRepository<UnitOfMeasure> repository,
    INameUniquenessChecker<UnitOfMeasure, UnitOfMeasureId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<CreateUnitOfMeasureCommand, Result<UnitOfMeasureDto>>
{
    public async Task<Result<UnitOfMeasureDto>> HandleAsync(
        CreateUnitOfMeasureCommand command,
        CancellationToken cancellationToken)
    {
        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, cancellationToken))
        {
            return Result<UnitOfMeasureDto>.Failure(
                IrtError.Conflict($"A unit of measure named '{command.Name}' already exists."));
        }

        var entity = UnitOfMeasure.Create(Name.Of(command.Name), command.Description);

        await repository.AddAsync(entity, cancellationToken);

        return Result<UnitOfMeasureDto>.Success(mapper.Map<UnitOfMeasureDto>(entity));
    }
}
