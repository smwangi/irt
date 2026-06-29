using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands.Handlers
{
    internal class DeleteDatasourceCommandHandler(
        IRepository<Core.Datasources.Datasource> datasourceRepository,
        IReadOnlyRepository<Core.Datasources.Datasource>repository)
         : ICommandHandler<DeleteDatasourceCommand, Result<Unit>>
    {

        public async Task<Result<Unit>> HandleAsync(DeleteDatasourceCommand request, CancellationToken cancellationToken)
        {
            var datasourceResult = await repository
                .FindByIdAsync(request.DatasourceId, cancellationToken);

            if (datasourceResult is null)
            {
                return Result<Unit>.Failure(IrtError.NotFound($"Datasource {request.DatasourceId} not found."));
            }
            
            datasourceResult.MarkAsDeleted();
            
            await datasourceRepository.UpdateAsync(datasourceResult, cancellationToken: cancellationToken);
            await datasourceRepository.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
