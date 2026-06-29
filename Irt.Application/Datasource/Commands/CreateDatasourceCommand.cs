using Irt.Application.Configuration.Commands;
using Irt.Application.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands
{
    public class CreateDatasourceCommand(DatasourceDto datasourceCreateRequest) : ICommand<Result<DatasourceDto>>
    {
        public DatasourceDto DatasourceCreateRequest { get; } = datasourceCreateRequest;
    }
}