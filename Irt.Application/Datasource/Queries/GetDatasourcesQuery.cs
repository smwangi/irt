using Irt.Application.Configuration.Queries;
using Irt.Application.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Queries
{
    public class GetDatasourceQuery : BaseODataQuery<Result<IQueryable<DatasourceDto>>>
    {
    }

    public class GetDatasourceByIdQuery(string id) : IQuery<Result<IQueryable<DatasourceDto>>>
    {
        public string Id { get; } = id;
    }
}
