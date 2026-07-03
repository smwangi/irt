namespace Irt.Application.IndicatorDefinitions;

public class IndicatorDefinitionDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public string ReportingScopeId { get; init; } = string.Empty;
    public string ReportingScopeName { get; init; } = string.Empty;

    public string UnitOfMeasureId { get; init; } = string.Empty;
    public string UnitOfMeasureName { get; init; } = string.Empty;

    public string IndicatorCategoryId { get; init; } = string.Empty;
    public string IndicatorCategoryName { get; init; } = string.Empty;

    public decimal MinThreshold { get; init; }
    public decimal MaxThreshold { get; init; }

    public string? Formula { get; init; }
    public string? FormulaDescription { get; init; }
    public string? Metadata { get; init; }
    public string? DPSIR { get; init; }
}
