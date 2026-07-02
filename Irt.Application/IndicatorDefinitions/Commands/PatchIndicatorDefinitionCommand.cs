using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands;

public sealed record PatchIndicatorDefinitionCommand(
    string? Name,
    string? Description,
    string? ReportingScopeId,
    string? UnitOfMeasureId,
    string? IndicatorCategoryId,
    decimal? MinThreshold,
    decimal? MaxThreshold,
    string? Formula,
    string? FormulaDescription,
    string? Metadata,
    string? DPSIR)
    : ICommand<Result<IndicatorDefinitionDto>>
{
    public string Id { get; init; } = default!;
}
