using Irt.Application.Configuration.Commands;
using Irt.Application.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands
{
    public class CreateDatasourceCommand(DatasourceDto datasourceCreateRequest) : CommandBase<Result<DatasourceDto>>
    {
        public DatasourceDto DatasourceCreateRequest { get; } = datasourceCreateRequest;
    }
}