using AutoMapper;
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

internal sealed class UpdateIndicatorDefinitionCommandHandler(
    IRepository<IndicatorDefinition> repository,
    IRepository<ReportingScope> reportingScopeRepository,
    IRepository<UnitOfMeasure> unitOfMeasureRepository,
    IRepository<IndicatorCategory> indicatorCategoryRepository,
    INameUniquenessChecker<IndicatorDefinition, IndicatorDefinitionId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<UpdateIndicatorDefinitionCommand, Result<IndicatorDefinitionDto>>
{
    public async Task<Result<IndicatorDefinitionDto>> HandleAsync(
        UpdateIndicatorDefinitionCommand command, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(
            IndicatorDefinitionId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"IndicatorDefinition with id {command.Id} not found."));
        }

        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, entity.Id, cancellationToken))
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.Conflict($"An indicator definition named '{command.Name}' already exists."));
        }

        var reportingScope = await reportingScopeRepository.GetByIdAsync(
            ReportingScopeId.Create(command.ReportingScopeId), cancellationToken);
        if (reportingScope is null)
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"ReportingScope with id {command.ReportingScopeId} not found."));
        }

        var unitOfMeasure = await unitOfMeasureRepository.GetByIdAsync(
            UnitOfMeasureId.Create(command.UnitOfMeasureId), cancellationToken);
        if (unitOfMeasure is null)
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"UnitOfMeasure with id {command.UnitOfMeasureId} not found."));
        }

        var indicatorCategory = await indicatorCategoryRepository.GetByIdAsync(
            IndicatorCategoryId.Create(command.IndicatorCategoryId), cancellationToken);
        if (indicatorCategory is null)
        {
            return Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"IndicatorCategory with id {command.IndicatorCategoryId} not found."));
        }

        try
        {
            entity.Update(
                Name.Of(command.Name),
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

        return Result<IndicatorDefinitionDto>.Success(mapper.Map<IndicatorDefinitionDto>(entity));
    }
}
