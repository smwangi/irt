using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Application.Exceptions;
using Irt.Application.Helpers;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SharedKernel;

namespace Irt.Application.Datasets.Commands.Handlers;

public class UpdateDatasetCommandHandler(
    IRepositoryFactory datasetRepository,
    IRepositoryFactory datasourceRepository,
    IRepositoryFactory indicatorDefinitionRepository,
    IMapper mapper)
    : ICommandHandler<UpdateDatasetCommand, Result<DatasetDto,string>>
{
    public async Task<Result<DatasetDto,string>> HandleAsync(UpdateDatasetCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var dataset = await datasetRepository
            .CreateFactory<Dataset>()
            .FindByIdAsync(request.DatasetDto.Id) ?? throw new NotFoundException("Dataset not found");

        var datasource = datasourceRepository
            .CreateFactory<Datasource>()
            .FindByIdAsync(request.DatasetDto.DatasourceId) ?? throw new NotFoundException("Datasource not found");

        var indicatorDefinition = indicatorDefinitionRepository
            .CreateFactory<IndicatorDefinition>()
            .FindByIdAsync(request.DatasetDto.IndicatorDefinitionId) ?? throw new NotFoundException("IndicatorDefinition not found");
        
        var hasChanges = dataset.Name.Value != request.DatasetDto.Name ||
                          dataset.Description != request.DatasetDto.Description;
        if (!hasChanges)
        {
            return Result<DatasetDto, string>.Success(mapper.Map<DatasetDto>(dataset), false);
        }
        
        var updatedDataset = await datasetRepository
            .CreateFactory<Dataset>()
            .UpdateAsync(dataset, cancellationToken);
        return Result<DatasetDto, string>.Success(mapper.Map<DatasetDto>(updatedDataset), true);
    }
}