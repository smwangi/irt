using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Application.Exceptions;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Application.Datasources.Commands.Handlers
{
    public class UpdateDatasourceCommandHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper)
        : ICommandHandler<UpdateDatasourceCommand, Result<DatasourceDto, string>>
    {
        public async Task<Result<DatasourceDto, string>> HandleAsync(UpdateDatasourceCommand request, CancellationToken cancellationToken)
        {
            var datasource = await datasourceRepository.CreateFactory<Datasource>().FindByIdAsync(
                new DatasourceId(request.DatasourceRequest.Id)) ?? throw new NotFoundException(request.DatasourceRequest.Id);

            // update datasource with new values
            datasource.UpdateDatasource(
                Name.Of(request.DatasourceRequest.Name),
                request.DatasourceRequest.Description,
                new DatasourceId(request.Id));

            await datasourceRepository.CreateFactory<Datasource>().UpdateAsync(datasource, cancellationToken);
            return Result<DatasourceDto, string>.Success(mapper.Map<DatasourceDto>(datasource));
        }
    }
}