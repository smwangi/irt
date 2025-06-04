using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Queries;

public class GetDatasetsByIdQuery(string id) : IQuery<Result<DatasetDto>>
{
    public string Id { get; } = id;
}

public class GetDatasetsQuery : IQuery<Result<List<DatasetDto>>> //IQuery<List<DatasetDto>>
{
}

public abstract class GetPagedDatasetQuery(int page, int size) : IQuery<List<DatasetDto>>
{
    public int Page { get; } = page;
    public int Size { get; } = size;
}

public abstract class GetDatasetsByTypeQuery(string type) : IQuery<Result<List<DatasetDto>>>
{
    public string Type { get; } = type;
}

public abstract class GetDatasetsByDatasourceIdQuery(string datasourceId) :
    IQuery<Result<List<DatasetDto>>>
{
    public string DatasourceId { get; } = datasourceId;
}

public class GetDatasetsByIndicatorDefinitionIdQuery(string indicatorDefinitionId) 
    : IQuery<List<DatasetDto>>
{
    public string IndicatorDefinitionId { get; } = indicatorDefinitionId;
}

public abstract class GetDatasetsByDatasourceIdAndTypeQuery(string type, string datasourceId) 
    : IQuery<Result<List<DatasetDto>>>
{
    public string Type { get; } = type;
    public string DatasourceId { get; } = datasourceId;
}