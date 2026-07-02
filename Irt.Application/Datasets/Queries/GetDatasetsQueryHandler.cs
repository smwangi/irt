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
