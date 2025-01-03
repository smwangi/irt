namespace Irt.Application.Datasets.Commands.Handlers;

using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Exceptions;
using Irt.Application.Configuration.Results;
using Irt.Application.Helpers;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SharedKernel;


public class UpdateDatasetCommandHandler : ICommandHandler<UpdateDatasetCommand, UpdateResult<string>>
{
    private readonly IDatasetRepository _datasetRepository;
    private readonly INameValidationChecker<Dataset> _datasetUniqueChecker;
    private readonly IDatasourceRepository _datasourceRepository;
    private readonly IIndicatorDefinitionRepository _indicatorDefinitionRepository;
    private readonly IMapper mapper;

    public UpdateDatasetCommandHandler(
        IDatasetRepository datasetRepository,
        INameValidationChecker<Dataset> nameValidationChecker,
        IDatasourceRepository datasourceRepository,
        IIndicatorDefinitionRepository indicatorDefinitionRepository,
        IMapper mapper)
    {
        _datasetRepository = datasetRepository;
        _datasetUniqueChecker = nameValidationChecker;
        _datasourceRepository = datasourceRepository;
        _indicatorDefinitionRepository = indicatorDefinitionRepository;
        this.mapper = mapper;
    }

    public async Task<UpdateResult<string>> Handle(UpdateDatasetCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var dataset = await RepositoryHelpers.GetOrThrowAsync(
            () => _datasetRepository.GetByIdAsync(new DatasetId(request.DatasetDto.Id), cancellationToken),
            request.DatasetDto.Id,
            nameof(Dataset));

        var datasource = await RepositoryHelpers.GetOrThrowAsync(
            () => _datasourceRepository.GetByIdAsync(new DatasourceId(request.DatasetDto.DatasourceId), cancellationToken),
            request.DatasetDto.DatasourceId,
            nameof(Datasource));

        var indicatorDefinition = await RepositoryHelpers.GetOrThrowAsync(
            () => _indicatorDefinitionRepository.GetByIdAsync(new IndicatorDefinitionId(request.DatasetDto.IndicatorDefinitionId), cancellationToken),
            request.DatasetDto.IndicatorDefinitionId,
            nameof(IndicatorDefinition));

        dataset.UpdateDataset(
            request.DatasetDto.Name,
            request.DatasetDto.Description,
            new DatasetId(request.DatasetDto.Id),
            datasource,
            indicatorDefinition,
            Enum.Parse<Core.Datasets.DatasetType>(request.DatasetDto.DatasetType.ToString()),
            _datasetUniqueChecker
        );

        await _datasetRepository.UpdateAsync(dataset, cancellationToken);
        return UpdateResult<string>.Success("Dataset updated successfully");
    }
}