using Irt.Application.Datasets.Commands;

namespace IrtWeb.Datasets;

public sealed record CreateDatasetRequest(
    string Name,
    string Description,
    string DatasourceId,
    string DatasetType,
    string IndicatorDefinitionId)
{
    public CreateDatasetCommand ToCommand()
        => new(Name, Description, DatasourceId, DatasetType, IndicatorDefinitionId);
}

public sealed record UpdateDatasetRequest(
    string Name,
    string Description,
    string DatasourceId,
    string DatasetType,
    string IndicatorDefinitionId,
    string? UserId,
    string? Username,
    string? Application,
    string? IpAddress)
{
    public UpdateDatasetCommand ToCommand(string id)
        => new(
            id,
            Name,
            Description,
            DatasourceId,
            DatasetType,
            IndicatorDefinitionId,
            UserId,
            Username,
            Application,
            IpAddress);
}
