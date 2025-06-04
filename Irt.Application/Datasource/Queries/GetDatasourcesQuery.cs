using Irt.Application.Configuration.Queries;
using Irt.Application.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Queries
{
    public class GetDatasourceQuery : IQuery<Result<List<DatasourceDto>>>
    {
    }

    public class GetDatasourceByIdQuery(string id) : IQuery<Result<DatasourceDto>>
    {
        public string Id { get; } = id;
    }
}