namespace Irt.Application.IndicatorDefinitions.Commands;

public interface IIndicatorDefinitionUpsertCommand
{
    string Name { get; }
    string Description { get; }
    string ReportingScopeId { get; }
    string UnitOfMeasureId { get; }
    string IndicatorCategoryId { get; }
    decimal MinThreshold { get; }
    decimal MaxThreshold { get; }
}
