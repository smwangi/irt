using System.Linq.Expressions;
using Irt.Application.Common;

namespace Irt.Application.Datasource.Queries;

public class DatasourceToDtoProjection : IProjection<Core.Datasources.Datasource, DatasourceDto>
{
    public Expression<Func<Core.Datasources.Datasource, DatasourceDto>> Expression => entity => new DatasourceDto
    (
        entity.Id,
        entity.Name.Value,
        entity.Description ?? string.Empty,
        entity.Source ?? string.Empty,
        entity.DatasourceType.ToString()
    );
}