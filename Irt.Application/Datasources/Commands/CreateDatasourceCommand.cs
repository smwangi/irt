using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasources.Commands
{
    public class CreateDatasourceCommand(DatasourceDto datasourceCreateRequest) : CommandBase<Result<DatasourceDto, string>>
    {
        public DatasourceDto DatasourceCreateRequest { get; } = datasourceCreateRequest;
    }
}