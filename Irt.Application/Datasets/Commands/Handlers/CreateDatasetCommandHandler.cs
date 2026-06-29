
using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.Datasets;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using CoreDatasource = Irt.Core.Datasources.Datasource;

namespace Irt.Application.Datasets.Commands.Handlers
{
    internal class CreateDatasetCommandHandler(
        IRepository<Dataset> datasetRepository,
        IReadOnlyRepository<Dataset> repository,
        IReadOnlyRepository<Core.Datasources.Datasource> datasourcesRepository,
        IReadOnlyRepository<IndicatorDefinition> indicatorDefinitionRepository,
        IMapper mapper) : ICommandHandler<CreateDatasetCommand, Result<DatasetDto>>
    {
        public async Task<Result<DatasetDto>> HandleAsync(
            CreateDatasetCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);
            var datasourceTask=  datasourcesRepository.FindByIdAsync(command.DatasourceId, cancellationToken);
            var indicatorDefinitionTask = indicatorDefinitionRepository.FindByIdAsync(command.IndicatorDefinitionId, cancellationToken);
                    
            await Task.WhenAll(datasourceTask, indicatorDefinitionTask);
        
            var datasource = await datasourceTask;
            var indicatorDefinition = await indicatorDefinitionTask;

            if (datasource == null)
            {
                return NotFound($"Datasource {command.DatasourceId} not found");
            }

            if (indicatorDefinition == null)
            {
                return NotFound($"Indicator definition {command.IndicatorDefinitionId} not found");
            }
            
            var dataset = Dataset.CreateDataset(
                name: Name.Of(command.Name),
                description: command.Description,
                datasetType: DatasetType.Parse(command.DatasetType), 
                indicatorDefinition: indicatorDefinition,
                source: datasource);
            
            await datasetRepository.AddAsync(dataset, cancellationToken);
            await datasetRepository.SaveChangesAsync(cancellationToken);

            return Result<DatasetDto>.Success(mapper.Map<DatasetDto>(dataset));
        }
        
        private static Result<DatasetDto> NotFound(string message)
            => Result<DatasetDto>.Failure(IrtError.NotFound(message));
    }
}
