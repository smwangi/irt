
using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;
using CoreDatasource = Irt.Core.Datasources.Datasource;

namespace Irt.Application.Datasets.Commands.Handlers
{
    internal class CreateDatasetCommandHandler(
        IRepositoryProvider repositoryProvider,
        IMapper mapper) : ICommandHandler<CreateDatasetCommand, Result<DatasetDto>>
    {
        public async Task<Result<DatasetDto>> HandleAsync(
            CreateDatasetCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command.Request);
            var indicatorDefinitionRepository = repositoryProvider.GetRepository<IndicatorDefinition>();
            var datasourceRepository = repositoryProvider.GetRepository<CoreDatasource>();
            var datasetRepository = repositoryProvider.GetRepository<Dataset>();
            
            return await datasourceRepository
                .FindByIdAsync(DatasourceId.Create(command.Request.DatasourceId))
                .EnsureAsync(d => d is not null && !d.IsDeleted, Error.NotFound("Datasource not found"))
                .BindAsync(datasource => indicatorDefinitionRepository
                    .FindByIdAsync(IndicatorDefinitionId.Create(command.Request.IndicatorDefinitionId)))
                .EnsureAsync(id => id is not null && !id.IsDeleted, Error.NotFound("IndicatorDefinition not found"))
                .BindAsync(async indDefn =>
                {
                    var dataset = Dataset.CreateDataset(
                        name: Name.Of(command.Request.Name),
                        description: command.Request.Description,
                        datasetType: (Core.Datasets.DatasetType)command.DatasetType,
                        indicatorDefinition: null,
                        source: null);
                    
                    await datasetRepository.AddAsync(dataset, cancellationToken);

                    return Result<DatasetDto>.Success(mapper.Map<DatasetDto>(dataset));
                });
        }
    }
}