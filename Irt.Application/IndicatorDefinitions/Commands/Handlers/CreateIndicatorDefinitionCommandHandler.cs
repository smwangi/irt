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

internal sealed class CreateIndicatorDefinitionCommandHandler(
    IRepository<IndicatorDefinition> repository,
    IRepository<ReportingScope> reportingScopeRepository,
    IRepository<UnitOfMeasure> unitOfMeasureRepository,
    IRepository<IndicatorCategory> indicatorCategoryRepository,
    INameUniquenessChecker<IndicatorDefinition, IndicatorDefinitionId> uniquenessChecker)
    : ICommandHandler<CreateIndicatorDefinitionCommand, Result<IndicatorDefinitionDto>>
{
    public async Task<Result<IndicatorDefinitionDto>> HandleAsync(
        CreateIndicatorDefinitionCommand command, CancellationToken cancellationToken)
    {
        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, cancellationToken))
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

        IndicatorDefinition entity;
        try
        {
            entity = IndicatorDefinition.CreateIndicatorDefinition(
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

        await repository.AddAsync(entity, cancellationToken);

        return Result<IndicatorDefinitionDto>.Success(
            IndicatorDefinitionDto.Projection.Compile()(entity));
    }
}
