using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Core.Datasets;
using Irt.Application.Helpers;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Queries;

internal class GetAllDatasetsQueryHandler(IRepositoryFactory repository, IMapper mapper) : IQueryHandler<GetDatasetsQuery ,Result<List<DatasetDto>>>
{
    public async Task<Result<List<DatasetDto>>> HandleAsync(GetDatasetsQuery request, CancellationToken cancellationToken)
    {
        return await repository.CreateFactory<Dataset>()
            .GetAllAsync()
            .MapAsync(mapper.Map<List<DatasetDto>>);
    }
}

internal class GetDatasetsByIdQueryHandler(IRepositoryFactory repository, IMapper mapper) : IQueryHandler<GetDatasetsByIdQuery, Result<DatasetDto>>
{

    public async Task<Result<DatasetDto>> HandleAsync(GetDatasetsByIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.CreateFactory<Dataset>()
            .FindByIdAsync(request.Id)
            .MapAsync(mapper.Map<DatasetDto>);
    }
}

internal class GetDatasetsByTypeQueryHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper) 
    : IQueryHandler<GetDatasetsByTypeQuery, Result<List<DatasetDto>>>
{
    public async Task<Result<List<DatasetDto>>> HandleAsync(GetDatasetsByTypeQuery request, CancellationToken cancellationToken)
    {
        return await repositoryProvider.GetRepository<Dataset>()
            .FilterAsync(x => x.DatasetType.ToString() == request.Type, cancellationToken)
            .MapAsync(mapper.Map<List<DatasetDto>>);
    }
}

internal class GetDatasetsByDatasourceIdQueryHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper) 
    : IQueryHandler<GetDatasetsByDatasourceIdQuery, Result<List<DatasetDto>>>
{
    public async Task<Result<List<DatasetDto>>> HandleAsync(
        GetDatasetsByDatasourceIdQuery request,
        CancellationToken cancellationToken)
    {
        return await repositoryProvider.GetRepository<Dataset>()
            .FilterAsync(x => x.Datasource.Id == request.DatasourceId, cancellationToken)
            .MapAsync(mapper.Map<List<DatasetDto>>);
    }
}

internal class GetDatasetsByDatasourceIdAndTypeQueryHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper) 
    : IQueryHandler<GetDatasetsByDatasourceIdAndTypeQuery, Result<List<DatasetDto>>>
{
    public async Task<Result<List<DatasetDto>>> HandleAsync(
        GetDatasetsByDatasourceIdAndTypeQuery request, 
        CancellationToken cancellationToken)
    {
        return await repositoryProvider.GetRepository<Dataset>().
            FilterAsync(
                x => x.Datasource.Id == request.DatasourceId 
                     && x.DatasetType.ToString() == request.Type, cancellationToken)
            .MapAsync(mapper.Map<List<DatasetDto>>);
    }
}