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

public abstract class GetPagedDatasetQuery(int page, int size) : IQuery<List<DatasetDto>>
{
    public int Page { get; } = page;
    public int Size { get; } = size;
}

public abstract class GetDatasetsByTypeQuery(string type) : BaseODataQuery<Result<IQueryable<DatasetDto>>>
{
    public string Type { get; } = type;
}

public abstract class GetDatasetsByDatasourceIdQuery(string datasourceId) :
    BaseODataQuery<Result<IQueryable<DatasetDto>>>
{
    public string DatasourceId { get; } = datasourceId;
}

public class GetDatasetsByIndicatorDefinitionIdQuery(string indicatorDefinitionId) 
    : IQuery<List<DatasetDto>>
{
    public string IndicatorDefinitionId { get; } = indicatorDefinitionId;
}

public abstract class GetDatasetsByDatasourceIdAndTypeQuery(string type, string datasourceId) 
    : BaseODataQuery<Result<IQueryable<DatasetDto>>>
{
    public string Type { get; } = type;
    public string DatasourceId { get; } = datasourceId;
}
