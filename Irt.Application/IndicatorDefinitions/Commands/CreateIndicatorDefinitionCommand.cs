using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands;

public sealed record CreateIndicatorDefinitionCommand(
    string Name,
    string Description,
    string ReportingScopeId,
    string UnitOfMeasureId,
    string IndicatorCategoryId,
    decimal MinThreshold,
    decimal MaxThreshold,
    string? Formula = null,
    string? FormulaDescription = null,
    string? Metadata = null,
    string? DPSIR = null)
    : ICommand<Result<IndicatorDefinitionDto>>, IIndicatorDefinitionUpsertCommand;