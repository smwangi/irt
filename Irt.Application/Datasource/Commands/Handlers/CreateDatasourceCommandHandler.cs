using AutoMapper;
using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.Core.Datasources;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using ValidationException = Irt.SharedKernel.ErrorHandling.Exceptions.ValidationException;

namespace Irt.Application.Datasource.Commands.Handlers
{
    internal class CreateDatasourceCommandHandler(
        IRepository<Core.Datasources.Datasource> repository,
        IMapper mapper,
        IValidator<DatasourceDto> validator,
        INameUniquenessChecker<Core.Datasources.Datasource, DatasourceId> uniquenessChecker) : ICommandHandler<CreateDatasourceCommand, Result<DatasourceDto>>
    {
        public async Task<Result<DatasourceDto>> HandleAsync(
            CreateDatasourceCommand command,
            CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await validator.ValidateAsync(command.DatasourceCreateRequest, cancellationToken);
            if (!validationResult.IsValid)
            {
                var dict = validationResult.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray());
                return Result<DatasourceDto>
                    .Failure(IrtError.FromException(
                        new ValidationException(dict)));
            }
            
            if (!Enum.TryParse(command.DatasourceCreateRequest.DatasourceType, out DatasourceType datasourceType))
            {
                return Result<DatasourceDto>
                    .Failure(IrtError.FromException(
                        new BadRequestException("Invalid datasource type.")));
            }
            
            if (!await uniquenessChecker.IsNameUniqueAsync(command.DatasourceCreateRequest.Name, cancellationToken))
            {
                return Result<DatasourceDto>.Failure(
                    IrtError.FromException(
                        new BadRequestException("Datasource name must be unique.")));
            }
            
            var datasource = Core.Datasources.Datasource.CreateDatasource(
                Name.Of(command.DatasourceCreateRequest.Name), 
                command.DatasourceCreateRequest.Description, 
                command.DatasourceCreateRequest.Source,
                datasourceType);

            var savedDatasource = await repository.AddAsync(datasource, cancellationToken);
            return Result<DatasourceDto>.Success(mapper.Map<DatasourceDto>(savedDatasource));
        }
    }
}
