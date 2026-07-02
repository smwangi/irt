using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Queries;

public class GetDatasetsByIdQuery(string id) : IQuery<Result<IQueryable<DatasetDto>>>
{
    public string Id { get; } = id;
}

public class GetDatasetsQuery : BaseODataQuery<Result<IQueryable<DatasetDto>>>
{
}
