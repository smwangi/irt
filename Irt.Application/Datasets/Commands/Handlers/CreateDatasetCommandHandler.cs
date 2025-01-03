
namespace Irt.Application.Datasets.Commands.Handlers
{
    using AutoMapper;
    using Irt.Application.Configuration.Commands;
    using Irt.Application.Configuration.Results;
    using Irt.Core.Datasets;
    using Irt.Core.Datasources;
    using Irt.Core.IndicatorDefinitions;
    using Irt.Core.SharedKernel;
    using Irt.Core.ValueObjects;

    public class CreateDatasetCommandHandler(
        IDatasetRepository datasetRepository,
        INameValidationChecker<Dataset> nameValidationChecker,
        IDatasourceRepository datasourceRepository,
        IIndicatorDefinitionRepository indicatorDefinitionRepository,
        IMapper mapper) : ICommandHandler<CreateDatasetCommand, Result<DatasetDto, string>>
    {
        private readonly IDatasetRepository _datasetRepository = datasetRepository;
        private readonly INameValidationChecker<Dataset> _datasetUniqueChecker = nameValidationChecker;
        private readonly IDatasourceRepository _datasourceRepository = datasourceRepository;

        private readonly IIndicatorDefinitionRepository _indicatorDefinitionRepository = indicatorDefinitionRepository;

        private readonly IMapper _mapper = mapper;

        public async Task<Result<DatasetDto,string>> Handle(CreateDatasetCommand request, CancellationToken cancellationToken)
        {
            var datasource = await _datasourceRepository.GetByIdAsync(new DatasourceId(request.DatasourceId), cancellationToken);
            var indicatorDefinition = await _indicatorDefinitionRepository.GetByIdAsync(new IndicatorDefinitionId(request.IndicatorDefinitionId), cancellationToken);
            var dataset = Dataset.CreateDataset(
                Name.Of(request.Name),
                request.Description,
                datasource,
                (Core.Datasets.DatasetType)request.DatasetType,
                indicatorDefinition,
                _datasetUniqueChecker);

            await _datasetRepository.AddAsync(dataset, cancellationToken);

            //return MapDatasetToResponse(dataset);
            var datasetDto = _mapper.Map<DatasetDto>(dataset);
            return Result<DatasetDto, string>.Ok(datasetDto);
        }
    }
}