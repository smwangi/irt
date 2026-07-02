using Irt.Application.IndicatorDefinitions.Commands;

namespace IrtWeb.IndicatorDefinitions;

public sealed record CreateIndicatorDefinitionRequest(
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
{
    public CreateIndicatorDefinitionCommand ToCommand()
        => new(
            Name,
            Description,
            ReportingScopeId,
            UnitOfMeasureId,
            IndicatorCategoryId,
            MinThreshold,
            MaxThreshold,
            Formula,
            FormulaDescription,
            Metadata,
            DPSIR);
}

public sealed record UpdateIndicatorDefinitionRequest(
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
{
    public UpdateIndicatorDefinitionCommand ToCommand(string id)
        => new(
            Name,
            Description,
            ReportingScopeId,
            UnitOfMeasureId,
            IndicatorCategoryId,
            MinThreshold,
            MaxThreshold,
            Formula,
            FormulaDescription,
            Metadata,
            DPSIR)
        {
            Id = id
        };
}

public sealed record PatchIndicatorDefinitionRequest(
    string? Name = null,
    string? Description = null,
    string? ReportingScopeId = null,
    string? UnitOfMeasureId = null,
    string? IndicatorCategoryId = null,
    decimal? MinThreshold = null,
    decimal? MaxThreshold = null,
    string? Formula = null,
    string? FormulaDescription = null,
    string? Metadata = null,
    string? DPSIR = null)
{
    public PatchIndicatorDefinitionCommand ToCommand(string id)
        => new(
            Name,
            Description,
            ReportingScopeId,
            UnitOfMeasureId,
            IndicatorCategoryId,
            MinThreshold,
            MaxThreshold,
            Formula,
            FormulaDescription,
            Metadata,
            DPSIR)
        {
            Id = id
        };
}
