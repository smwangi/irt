using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasources.Commands
{
    public class CreateDatasourceCommand(DatasourceDto datasourceCreateRequest) : CommandBase<Result<DatasourceDto>>
    {
        public DatasourceDto DatasourceCreateRequest { get; } = datasourceCreateRequest;
    }
}