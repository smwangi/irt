using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.Datasets;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.Extensions;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;
using CoreDatasource = Irt.Core.Datasources.Datasource;

namespace Irt.Application.Datasets.Commands.Handlers;

public class UpdateDatasetCommandHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper)
    : ICommandHandler<UpdateDatasetCommand, Result<DatasetDto>>
{
    public async Task<Result<DatasetDto>> HandleAsync(
        UpdateDatasetCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var datasetRepository = repositoryProvider.GetRepository<Dataset>();
        var datasourceRepository = repositoryProvider.GetRepository<CoreDatasource>();
        var indicatorDefinitionRepository = repositoryProvider.GetRepository<IndicatorDefinition>();

        return await datasetRepository
            .FindByIdAsync(request.DatasetDto.Id)
            .BindAsync(async dataset =>
            {
                var datasource = await datasourceRepository
                    .FindByIdAsync(request.DatasetDto.DatasourceId);


                var indicatorDefinition = await indicatorDefinitionRepository
                    .FindByIdAsync(request.DatasetDto.IndicatorDefinitionId);

                var datasetType =
                    EnumExtensions
                        .TryParseEnum<Core.Datasets.DatasetType>(request.DatasetDto.DatasetType.ToString());
                if (datasetType.IsFailure)
                {
                    return Result<DatasetDto>.Failure(datasetType.IrtError!);
                }

                dataset.WithUpdatedDataset(
                    name: Name.Of(request.DatasetDto.Name),
                    description: request.DatasetDto.Description,
                    datasource: datasource.Unwrap(),
                    indicatorDefinition: indicatorDefinition.Unwrap(),
                    datasetType: datasetType.Value);

                var updatedDataset = await datasetRepository
                    .UpdateAsync(dataset, cancellationToken);

                return Result<DatasetDto>.Success(mapper.Map<DatasetDto>(updatedDataset));
            });
    }
}