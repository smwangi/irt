using Irt.Application.Configuration.Queries;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasources
{
    public class GetDatasourcesQuery : IQuery<Result<List<DatasourceDto>, string>>
    {
    }

    public class GetDatasourcesByIdQuery(string id) : IQuery<DatasourceDto>
    {
        public string Id { get; } = id;
    }
}