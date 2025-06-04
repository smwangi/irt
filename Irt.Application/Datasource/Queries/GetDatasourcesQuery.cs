using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasources.Queries
{
    public class GetDatasourceQuery : IQuery<Result<List<DatasourceDto>>>
    {
    }

    public class GetDatasourceByIdQuery(string id) : IQuery<Result<DatasourceDto>>
    {
        public string Id { get; } = id;
    }
}