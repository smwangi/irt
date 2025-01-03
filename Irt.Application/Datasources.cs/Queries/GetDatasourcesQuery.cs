using Irt.Application.Configuration.Queries;

namespace Irt.Application.Datasources
{
    public class GetDatasourcesQuery : IQuery<List<DatasourceDto>>
    {
    }

    public class GetDatasourcesByIdQuery(string id) : IQuery<DatasourceDto>
    {
        public string Id { get; set; } = id;
    }
}