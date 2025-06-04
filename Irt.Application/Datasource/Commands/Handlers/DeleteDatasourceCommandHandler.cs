using Irt.Application.Configuration.Commands;
using Irt.Core.Datasources;

namespace Irt.Application.Datasources.Commands
{
    public class DeleteDatasourceCommandHandler(IDatasourceRepository dataSourceRepository)
         : ICommandHandler<DeleteDatasourceCommand, DeleteDatasourceResult>
    {
        private readonly IDatasourceRepository _datasourceRepository = dataSourceRepository;

        public async Task<DeleteDatasourceResult> HandleAsync(DeleteDatasourceCommand request, CancellationToken cancellationToken)
        {
            var datasource = await _datasourceRepository.GetByIdAsync(DatasourceId.Create(request.DatasourceId), cancellationToken);
            if (datasource is null)
            {
                return new DeleteDatasourceResult(false);
            }

            //datasource.Delete();
            await _datasourceRepository.DeleteAsync(datasource, cancellationToken);
            return new DeleteDatasourceResult(true);
        }
    }
}