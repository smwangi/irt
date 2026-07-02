using System.Linq.Expressions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.Datasets;
using Irt.SharedKernel.Extensions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Queries;

internal class GetAllDatasetsQueryHandler(
    IReadOnlyRepository<Dataset> repository,
    IProjection<Dataset, DatasetDto> projection)
    : IODataQueryHandler<GetDatasetsQuery, Result<IQueryable<DatasetDto>>>
{
    public Task<Result<IQueryable<DatasetDto>>> HandleAsync(GetDatasetsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            repository
                .QuerySafely(projection.Expression)
                .Bind(q => q.ApplyODataSafely(request.Options)));

    }
}

internal class GetDatasetsByIdQueryHandler(
    IReadOnlyRepository<Dataset> repository, 
    IProjection<Dataset, DatasetDto> projection)
    : IQueryHandler<GetDatasetsByIdQuery, Result<IQueryable<DatasetDto>>>
{

    public Task<Result<IQueryable<DatasetDto>>> HandleAsync(GetDatasetsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = repository.QueryById(request.Id, projection.Expression);
        return Task.FromResult(Result<IQueryable<DatasetDto>>.Success(result));
            
    }
}

internal class GetDatasetsByTypeQueryHandler(
    IReadOnlyRepository<Dataset> repository,
    IProjection<Dataset, DatasetDto> projection) 
    : IODataQueryHandler<GetDatasetsByTypeQuery, Result<IQueryable<DatasetDto>>>
{
    public Task<Result<IQueryable<DatasetDto>>> HandleAsync(GetDatasetsByTypeQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Dataset, bool>> predicate = r => r.DatasetType.ToString() == request.Type;
        var result = repository
            .QuerySafely(predicate, projection.Expression)
            .Bind(q => q.ApplyODataSafely(request.Options));
        
        return Task.FromResult(result);
    }
}

internal class GetDatasetsByDatasourceIdQueryHandler(
    IReadOnlyRepository<Dataset> repository,
    IProjection<Dataset, DatasetDto> projection) 
    : IODataQueryHandler<GetDatasetsByDatasourceIdQuery, Result<IQueryable<DatasetDto>>>
{
    public Task<Result<IQueryable<DatasetDto>>> HandleAsync(GetDatasetsByDatasourceIdQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Dataset, bool>> filter = d => d.Datasource.Id == request.DatasourceId;
        return Task.FromResult(
            repository.QuerySafely(filter, projection.Expression)
                .Bind(q => q.ApplyODataSafely(request.Options)));
    }
}

internal class GetDatasetsByDatasourceIdAndTypeQueryHandler(
    IReadOnlyRepository<Dataset> repository,
    IProjection<Dataset, DatasetDto> projection) 
    : IQueryHandler<GetDatasetsByDatasourceIdAndTypeQuery, Result<IQueryable<DatasetDto>>>
{
    public Task<Result<IQueryable<DatasetDto>>> HandleAsync(GetDatasetsByDatasourceIdAndTypeQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Dataset, bool>> filter = (d => d.Datasource.Id == request.DatasourceId && d.DatasetType.ToString() == request.Type);
        return Task.FromResult(
            repository
                .QuerySafely(filter, projection.Expression)
                .Bind(q => q.ApplyODataSafely(request.Options)));
    }
}
