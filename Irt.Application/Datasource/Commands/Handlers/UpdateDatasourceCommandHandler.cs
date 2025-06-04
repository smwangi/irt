using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands.Handlers
{
    public class UpdateDatasourceCommandHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper)
        : ICommandHandler<UpdateDatasourceCommand, Result<DatasourceDto>>
    {
        public async Task<Result<DatasourceDto>> HandleAsync(
            UpdateDatasourceCommand request, 
            CancellationToken cancellationToken)
        {
            return await datasourceRepository
                .CreateFactory<Core.Datasources.Datasource>()
                .FindByIdAsync(request.Id)
                .EnsureAsync(d => d != null, Error.NotFound($"Datasource with id {request.Id} not found."))
                .BindAsync(async datasource =>
                {
                    if (request.DatasourceRequest.Id != null)
                    {
                        datasource.WithUpdatedDatasource(
                            name: Name.Of(request.DatasourceRequest.Name),
                            request.DatasourceRequest.Description);
                    }
                        

                    var updatedDatasource = await datasourceRepository
                        .CreateFactory<Core.Datasources.Datasource>()
                        .UpdateAsync(datasource, cancellationToken);
                    
                    return Result<DatasourceDto>.Success(mapper.Map<DatasourceDto>(updatedDatasource));
                });
        }
    }
}