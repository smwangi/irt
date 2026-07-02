using System.Linq.Expressions;
using Irt.Application.Common;
using Irt.Core.Datasets;

namespace Irt.Application.Datasets.Queries;

public class DatasetToDtoProjection : IProjection<Dataset, DatasetDto>
{
    public Expression<Func<Dataset, DatasetDto>> Expression => entity => new DatasetDto(
        entity.Id,
        entity.Name.Value,
        entity.Description ?? string.Empty,
        entity.Datasource.Id,
        entity.DatasetType.Value,
        entity.IndicatorDefinition.Id);
}
