
using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Application.Exceptions;
using Irt.Application.Helpers;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using BusinessRuleValidationException = Irt.Core.Datasources.BusinessRuleValidationException;

namespace Irt.Application.Datasets.Commands.Handlers
{
    internal class CreateDatasetCommandHandler(
        IRepositoryFactory datasetRepository,
        IRepositoryFactory datasourceRepository,
        IRepositoryFactory indicatorDefinitionRepository,
        IMapper mapper) : ICommandHandler<CreateDatasetCommand, Result<DatasetDto, string>>
    {
        public async Task<Result<DatasetDto,string>> HandleAsync(
            CreateDatasetCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command.Request);
            var datasource = await datasourceRepository
                .CreateFactory<Datasource>()
                .FilterByIdAsync(command.Request.DatasourceId);
            
            if (datasource == null || datasource.IsDeleted)
            {
                throw new NotFoundException("Datasource not found");
            }
            
            var indicatorDefinition = await indicatorDefinitionRepository.CreateFactory<IndicatorDefinition>().FindByIdAsync(command.Request.IndicatorDefinitionId);
            
            if (indicatorDefinition == null || indicatorDefinition.IsDeleted)
            {
                throw new NotFoundException("IndicatorDefinition not found");
            }
            var name = Name.Of(command.Request.Name);
            //var nameRule = new NameMustBeUniqueRule<Dataset>(name, datasetRepository, x => x.Name);
            var compositeRule = new CompositeRule(new List<IBusinessRule>
            {
                //nameRule

            });
            if (await compositeRule.IsBrokenAsync())
            {
                throw new BusinessRuleValidationException(compositeRule.Message);
            }
            /*if (await nameRule.IsBrokenAsync())
            {
                throw new BusinessRuleValidationException(nameRule.Message);
            }*/
            var dataset = Dataset.CreateDataset(
                Name.Of(command.Request.Name),
                command.Request.Description,
                datasource,
                (Core.Datasets.DatasetType)command.DatasetType,
                indicatorDefinition);

            await datasetRepository.CreateFactory<Dataset>().AddAsync(dataset, cancellationToken);

            //return MapDatasetToResponse(dataset);
            var datasetDto = mapper.Map<DatasetDto>(dataset);
            return Result<DatasetDto, string>.Success(datasetDto);
        }
    }
}