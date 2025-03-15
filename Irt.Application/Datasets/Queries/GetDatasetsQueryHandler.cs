using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Core.Datasets;
using Irt.Application.Configuration.Results;
using Irt.Application.Helpers;
using Irt.Core.SharedKernel;
namespace Irt.Application.Datasets.Queries;

internal class GetAllDatasetsQueryHandler(IRepositoryFactory repository, IMapper mapper) : IQueryHandler<GetDatasetsQuery ,Result<List<DatasetDto>, string>>
{
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<DatasetDto>, string>> HandleAsync(GetDatasetsQuery request, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM irt.irt.datasets";
        //var datasets = await repository..GetAllAsync(query, cancellationToken);
        //var datasetDtos = _mapper.Map<List<DatasetDto>>(datasets);
        //return Result<List<DatasetDto>, string>.Ok(_mapper.Map<List<DatasetDto>>(datasets));
        //return Result<string, List<DatasetDto>>.Ok(datasetDtos);
        var result = await repository.CreateFactory<Dataset>().
            GetAllAsync(query, cancellationToken);
        return result.Count == 0 ? Result<List<DatasetDto>, string>.Failure("Datasets not found") : Result<List<DatasetDto>, string>.Success(_mapper.Map<List<DatasetDto>>(result));
    }
}

internal class GetDatasetsByIdQueryHandler(IRepositoryFactory repository, IMapper mapper) : IQueryHandler<GetDatasetsByIdQuery, Result<DatasetDto, string>>
{

    public async Task<Result<DatasetDto, string?>> HandleAsync(GetDatasetsByIdQuery request, CancellationToken cancellationToken)
    {
        var dataset = await repository.CreateFactory<Dataset>()
            .FindByIdAsync(request.Id);
        
        return dataset == null
            ? Result<DatasetDto, string>.Failure("Dataset not found")
            : Result<DatasetDto, string>.Success(mapper.Map<DatasetDto>(dataset));
    }
}

internal class GetDatasetsByTypeQueryHandler(IRepository<Dataset> datasetRepository, IMapper mapper) : IQueryHandler<GetDatasetsByTypeQuery, List<DatasetDto>>
{
    private readonly IRepository<Dataset> _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> HandleAsync(GetDatasetsByTypeQuery request, CancellationToken cancellationToken)
    {
        var whereClause = $"type = '{request.Type}'";
        var datasets = await _datasetRepository.GetAllAsync("", cancellationToken);//GetByTypeAsync(Enum.Parse<Core.Datasets.DatasetType>(request.Type), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}

internal class GetDatasetsByDatasourceIdQueryHandler(IRepository<Dataset> datasetRepository, IMapper mapper) : IQueryHandler<GetDatasetsByDatasourceIdQuery, List<DatasetDto>>
{
    private readonly IRepository<Dataset> _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> HandleAsync(GetDatasetsByDatasourceIdQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetAllAsync("", cancellationToken);//GetByDatasourceIdAsync(new DatasourceId(request.DatasourceId), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}

internal class GetDatasetsByDatasourceIdAndTypeQueryHandler(IRepository<Dataset> datasetRepository, IMapper mapper) : IQueryHandler<GetDatasetsByDatasourceIdAndTypeQuery, List<DatasetDto>>
{
    private readonly IRepository<Dataset> _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> HandleAsync(GetDatasetsByDatasourceIdAndTypeQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetAllAsync("", cancellationToken);//GetByDatasourceIdAndTypeAsync(new DatasourceId(request.DatasourceId), Enum.Parse<Core.Datasets.DatasetType>(request.Type), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}