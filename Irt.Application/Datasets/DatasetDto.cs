namespace Irt.Application.Datasets;

using System.ComponentModel.DataAnnotations;
public record DatasetDto(
    string Id,
    [Required]
    [StringLength(50, MinimumLength = 3)]
    string Name,
    string Description,
    string DatasourceId,
    DatasetType DatasetType,
    string IndicatorDefinitionId,
    DateTime? CreatedAt,
    DateTime? LastModifiedAt,
    string LastModifiedBy,
    string CreatedBy
);