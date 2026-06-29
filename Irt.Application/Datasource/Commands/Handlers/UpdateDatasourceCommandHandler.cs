using AutoMapper;
using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands.Handlers
{
    internal class UpdateDatasourceCommandHandler(
        IRepository<Core.Datasources.Datasource> datasourceRepository,
        IReadOnlyRepository<Core.Datasources.Datasource> repository,
        IMapper mapper)
        : ICommandHandler<UpdateDatasourceCommand, Result<DatasourceDto>>
    {
        public async Task<Result<DatasourceDto>> HandleAsync(
            UpdateDatasourceCommand request, 
            CancellationToken cancellationToken)
        {
            var existingDatasource = await repository.FindByIdAsync(request.Id, cancellationToken);
            
            if (existingDatasource is null)
            {
                return Result<DatasourceDto>.Failure(IrtError.NotFound($"Datasource {request.Id} not found."));
            }
            
            existingDatasource.UpdateDatasource(
                Name.Of(request.Name),
                request.Description);
            
            AuditRegistrar.RegisterModificationOnly(existingDatasource, command: request);
            var updatedDatasource = await datasourceRepository.UpdateAsync(existingDatasource, cancellationToken);
            await datasourceRepository.SaveChangesAsync(cancellationToken);
            return Result<DatasourceDto>.Success(mapper.Map<DatasourceDto>(updatedDatasource));
        }
    }
}
