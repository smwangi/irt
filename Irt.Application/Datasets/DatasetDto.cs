namespace Irt.Application.Datasets;

using System.ComponentModel.DataAnnotations;
public record DatasetDto(
    string Id,
    string Name,
    string Description,
    string DatasourceId,
    string DatasetType,
    string IndicatorDefinitionId
);