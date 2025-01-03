using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Core.Datasources;
using Irt.Core.SharedKernel;

namespace Irt.Application.Datasources.Commands
{
    internal sealed class CreateDatasourceCommandHandler(
        IDatasourceRepository datasourceRepository,
        INameValidationChecker<Datasource> datasourceUniqueChecker) : ICommandHandler<CreateDatasourceCommand, Result<DatasourceDto, string>>
    {
        private readonly INameValidationChecker<Datasource> _datasourceUniqueChecker = datasourceUniqueChecker;
        public async Task<Result<DatasourceDto, string>> Handle(CreateDatasourceCommand request, CancellationToken cancellationToken)
        {
            var datasource = Datasource.CreateDatasource(
                request.Name, 
                request.Description, 
                request.Source,
                Enum.Parse<DatasourceType>(request.DatasourceType),
                _datasourceUniqueChecker);

            await datasourceRepository.AddAsync(datasource, cancellationToken);
            return Result<DatasourceDto, string>.Ok(new DatasourceDto(
                Id: datasource.Id.Value.ToString(),
                Name: datasource.Name.Value,
                Description: datasource.Description?? string.Empty,
                Source: datasource.Source?? string.Empty,
                DatasourceType: datasource.DatasourceType.ToString(),
                CreatedAt: datasource.CreatedAt,
                LastModifiedAt: datasource.LastModifiedAt,
                LastModifiedBy: datasource.LastModifiedBy,
                CreatedBy: datasource.CreatedBy));
        }
    }
}