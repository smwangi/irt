using Irt.Application.Configuration.Queries;

namespace Irt.Application.Datasets.Queries;

using Irt.Application.Configuration.Results;

public class GetDatasetsByIdQuery(string id) : IQuery<Result<DatasetDto, string>>
{
    public string Id { get; } = id;
}

public class GetDatasetsQuery : IQuery<Result<List<DatasetDto>, string>> //IQuery<List<DatasetDto>>
{
}

public class GetPagedDatasetQuey(int page, int size) : IQuery<List<DatasetDto>>
{
    public int Page { get; } = page;
    public int Size { get; } = size;
}

public class GetDatasetsByTypeQuery(string type) : IQuery<List<DatasetDto>>
{
    public string Type { get; } = type;
}

public class GetDatasetsByDatasourceIdQuery(string datasourceId) : IQuery<List<DatasetDto>>
{
    public string DatasourceId { get; } = datasourceId;
}

public class GetDatasetsByIndicatorDefinitionIdQuery(string indicatorDefinitionId) : IQuery<List<DatasetDto>>
{
    public string IndicatorDefinitionId { get; } = indicatorDefinitionId;
}

public class GetDatasetsByDatasourceIdAndTypeQuery(string type, string datasourceId) : IQuery<List<DatasetDto>>
{
    public string Type { get; } = type;
    public string DatasourceId { get; } = datasourceId;
}