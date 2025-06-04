using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasources.Commands.Handlers
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
                .CreateFactory<Datasource>()
                .FindByIdAsync(request.Id)
                .EnsureAsync(d => d != null, Error.NotFound($"Datasource with id {request.Id} not found."))
                .BindAsync(async datasource =>
                {
                    if (request.DatasourceRequest.Id != null)
                    {
                        datasource.UpdateDatasource(
                            name: Name.Of(request.DatasourceRequest.Name),
                            request.DatasourceRequest.Description,
                            DatasourceId.Create(request.DatasourceRequest.Id));
                    }
                        

                    var updatedDatasource = await datasourceRepository
                        .CreateFactory<Datasource>()
                        .UpdateAsync(datasource, cancellationToken);
                    
                    return Result<DatasourceDto>.Success(mapper.Map<DatasourceDto>(updatedDatasource));
                });
        }
    }
}