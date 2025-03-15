using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Application.Datasources.Validators;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.Core.ValueObjects;

namespace Irt.Application.Datasources.Commands.Handlers
{
    internal sealed class CreateDatasourceCommandHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : ICommandHandler<CreateDatasourceCommand, Result<DatasourceDto, string>>
    {
        public async Task<Result<DatasourceDto, string>> HandleAsync(CreateDatasourceCommand command, CancellationToken cancellationToken)
        {
            // Validate the command
            var validator = new DatasourceDtoValidator();
            var validationResult = await validator.ValidateAsync(command.DatasourceCreateRequest);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<DatasourceDto, string>.Failure(errors);
            }
            
            if (!Enum.TryParse(command.DatasourceCreateRequest.DatasourceType, out DatasourceType datasourceType))
            {
                return Result<DatasourceDto, string>.Failure($"Invalid datasource type: {command.DatasourceCreateRequest.DatasourceType}");
            }
            
            var datasource = Datasource.CreateDatasource(
                Name.Of(command.DatasourceCreateRequest.Name), 
                command.DatasourceCreateRequest.Description, 
                command.DatasourceCreateRequest.Source,
                datasourceType);

            await datasourceRepository.CreateFactory<Datasource>().AddAsync(datasource, cancellationToken);
            return Result<DatasourceDto, string>.Success(mapper.Map<DatasourceDto>(datasource));
        }
    }
}