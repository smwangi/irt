using Irt.Application.Datasource;
using Irt.Application.Datasource.Commands;

namespace IrtWeb.Datasources;

public sealed record CreateDatasourceRequest(
    string Name,
    string Description,
    string Source,
    string DatasourceType)
{
    public CreateDatasourceCommand ToCommand()
        => new(new DatasourceDto(
            Id: string.Empty,
            Name,
            Description,
            Source,
            DatasourceType));
}

public sealed record UpdateDatasourceRequest(
    string Name,
    string Description,
    string Source,
    string DatasourceType)
{
    public UpdateDatasourceCommand ToCommand(string id)
        => new(id, Name, Description, Source, DatasourceType);
}
