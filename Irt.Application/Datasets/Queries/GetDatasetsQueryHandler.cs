using AutoMapper;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Application.Configuration.Results;
using MediatR;

namespace Irt.Application.Datasets.Queries;

internal class GetAllDatasetsQueryHandler(IDatasetRepository datasetRepository, IMapper mapper) : IRequestHandler<GetDatasetsQuery ,Result<List<DatasetDto>, string>>
{
    private readonly IDatasetRepository _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<DatasetDto>, string>> Handle(GetDatasetsQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetAllAsync(cancellationToken);
        var datasetDtos = _mapper.Map<List<DatasetDto>>(datasets);
        return Result<List<DatasetDto>, string>.Ok(_mapper.Map<List<DatasetDto>>(datasets));
        //return Result<string, List<DatasetDto>>.Ok(datasetDtos);
    }
}

internal class GetDatasetsByIdQueryHandler(IDatasetRepository datasetRepository, IMapper mapper) : IRequestHandler<GetDatasetsByIdQuery, Result<DatasetDto, string>>
{
    private readonly IDatasetRepository _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<DatasetDto, string>> Handle(GetDatasetsByIdQuery request, CancellationToken cancellationToken)
    {
        /*var dataset = await _datasetRepository.GetByIdAsync(new DatasetId(request.Id), cancellationToken) ?? throw new Exception($"Dataset not found {request.Id}");
        return _mapper.Map<DatasetDto>(dataset);*/
        var dataset = await _datasetRepository.GetByIdAsync(new DatasetId(request.Id), cancellationToken);
        if (dataset == null)
            return Result<DatasetDto, string>.Fail("Dataset not found");

        return Result<DatasetDto, string>.Ok(_mapper.Map<DatasetDto>(dataset));
    }
}

internal class GetDatasetsByTypeQueryHandler(IDatasetRepository datasetRepository, IMapper mapper) : IRequestHandler<GetDatasetsByTypeQuery, List<DatasetDto>>
{
    private readonly IDatasetRepository _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> Handle(GetDatasetsByTypeQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetByTypeAsync(Enum.Parse<Core.Datasets.DatasetType>(request.Type), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}

internal class GetDatasetsByDatasourceIdQueryHandler(IDatasetRepository datasetRepository, IMapper mapper) : IRequestHandler<GetDatasetsByDatasourceIdQuery, List<DatasetDto>>
{
    private readonly IDatasetRepository _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> Handle(GetDatasetsByDatasourceIdQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetByDatasourceIdAsync(new DatasourceId(request.DatasourceId), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}

internal class GetDatasetsByDatasourceIdAndTypeQueryHandler(IDatasetRepository datasetRepository, IMapper mapper) : IRequestHandler<GetDatasetsByDatasourceIdAndTypeQuery, List<DatasetDto>>
{
    private readonly IDatasetRepository _datasetRepository = datasetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<DatasetDto>> Handle(GetDatasetsByDatasourceIdAndTypeQuery request, CancellationToken cancellationToken)
    {
        var datasets = await _datasetRepository.GetByDatasourceIdAndTypeAsync(new DatasourceId(request.DatasourceId), Enum.Parse<Core.Datasets.DatasetType>(request.Type), cancellationToken);
        return _mapper.Map<List<DatasetDto>>(datasets);
    }
}