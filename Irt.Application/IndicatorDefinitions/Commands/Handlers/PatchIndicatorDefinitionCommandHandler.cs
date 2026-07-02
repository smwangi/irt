using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.ReportingScopes;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands.Handlers;

internal sealed class PatchIndicatorDefinitionCommandHandler(
    IRepository<IndicatorDefinition> repository,
    IRepository<ReportingScope> reportingScopeRepository,
    IRepository<UnitOfMeasure> unitOfMeasureRepository,
    IRepository<IndicatorCategory> indicatorCategoryRepository,
    INameUniquenessChecker<IndicatorDefinition, IndicatorDefinitionId> uniquenessChecker)
    : ICommandHandler<PatchIndicatorDefinitionCommand, Result<IndicatorDefinitionDto>>
{
    public async Task<Result<IndicatorDefinitionDto>> HandleAsync(
        PatchIndicatorDefinitionCommand command, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(
            IndicatorDefinitionId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"IndicatorDefinition with id {command.Id} not found."));
        }

        Name? newName = null;
        if (!string.IsNullOrWhiteSpace(command.Name))
        {
            if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, entity.Id, cancellationToken))
            {
                return Result<IndicatorDefinitionDto>.Failure(
                    IrtError.Conflict($"An indicator definition named '{command.Name}' already exists."));
            }
            newName = Name.Of(command.Name);
        }

        ReportingScope? reportingScope = null;
        if (!string.IsNullOrWhiteSpace(command.ReportingScopeId))
        {
            reportingScope = await reportingScopeRepository.GetByIdAsync(
                ReportingScopeId.Create(command.ReportingScopeId), cancellationToken);
            if (reportingScope is null)
            {
                return Result<IndicatorDefinitionDto>.Failure(
                    IrtError.NotFound($"ReportingScope with id {command.ReportingScopeId} not found."));
            }
        }

        UnitOfMeasure? unitOfMeasure = null;
        if (!string.IsNullOrWhiteSpace(command.UnitOfMeasureId))
        {
            unitOfMeasure = await unitOfMeasureRepository.GetByIdAsync(
                UnitOfMeasureId.Create(command.UnitOfMeasureId), cancellationToken);
            if (unitOfMeasure is null)
            {
                return Result<IndicatorDefinitionDto>.Failure(
                    IrtError.NotFound($"UnitOfMeasure with id {command.UnitOfMeasureId} not found."));
            }
        }

        IndicatorCategory? indicatorCategory = null;
        if (!string.IsNullOrWhiteSpace(command.IndicatorCategoryId))
        {
            indicatorCategory = await indicatorCategoryRepository.GetByIdAsync(
                IndicatorCategoryId.Create(command.IndicatorCategoryId), cancellationToken);
            if (indicatorCategory is null)
            {
                return Result<IndicatorDefinitionDto>.Failure(
                    IrtError.NotFound($"IndicatorCategory with id {command.IndicatorCategoryId} not found."));
            }
        }

        try
        {
            entity.Patch(
                newName,
                command.Description,
                reportingScope,
                unitOfMeasure,
                indicatorCategory,
                command.MinThreshold,
                command.MaxThreshold,
                command.Formula,
                command.FormulaDescription,
                command.Metadata,
                command.DPSIR);
        }
        catch (ArgumentException ex)
        {
            return Result<IndicatorDefinitionDto>.Failure(IrtError.Validation(ex.Message));
        }

        return Result<IndicatorDefinitionDto>.Success(
            IndicatorDefinitionDto.Projection.Compile()(entity));
    }
}
