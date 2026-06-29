using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Datasource;
using Irt.Core.Datasets;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using CoreDatasource = Irt.Core.Datasources.Datasource;

namespace Irt.Application.Datasets.Commands.Handlers;

public class UpdateDatasetCommandHandler(
    IRepositoryProvider repositoryProvider,
    IReadOnlyRepository<Dataset> repository,
    IReadOnlyRepository<Core.Datasources.Datasource> datasourcesRepository,
    IReadOnlyRepository<IndicatorDefinition> indicatorDefinitionRepository,
    IMapper mapper)
    : ICommandHandler<UpdateDatasetCommand, Result<DatasetDto>>
{
    public async Task<Result<DatasetDto>> HandleAsync(
        UpdateDatasetCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var datasetRepository = repositoryProvider.GetRepository<Dataset>();
        
        var datasourceTask=  datasourcesRepository.FindByIdAsync(request.DatasourceId, cancellationToken);
        var indicatorDefinitionTask = indicatorDefinitionRepository.FindByIdAsync(request.IndicatorDefinitionId, cancellationToken);
        var existingDatasetTask = repository.FindByIdAsync(request.Id, cancellationToken);
        
        await Task.WhenAll(datasourceTask, indicatorDefinitionTask, existingDatasetTask);
        
        var existingDataset = await existingDatasetTask;
        var datasource = await datasourceTask;
        var indicatorDefinition = await indicatorDefinitionTask;
        
        if (datasource == null)
        {
            return NotFound($"Datasource {request.DatasourceId} not found.");
        }

        if (indicatorDefinition == null)
        {
            return NotFound($"Indicator definition {request.IndicatorDefinitionId} not found.");
        }
        
        if (existingDataset is null)
        {
            return NotFound($"Dataset with id {request.Id} not found");
        }

        var datasetType = DatasetType.Parse(request.DatasetType);
        existingDataset.WithUpdatedDataset(
            name: Name.Of(request.Name),
            description: request.Description,
            datasource: datasource,
            indicatorDefinition: indicatorDefinition,
            datasetType: datasetType);

        var updatedDataset = await datasetRepository
            .UpdateAsync(existingDataset, cancellationToken);

        return Result<DatasetDto>.Success(mapper.Map<DatasetDto>(updatedDataset));
    }
    
    private static Result<DatasetDto> NotFound(string message)
        => Result<DatasetDto>.Failure(IrtError.NotFound(message));
}