using Irt.Core.ValueObjects;
using MediatR;

namespace Irt.Core.Datasources
{
    public interface IDatasourceRepository
    {
        Task<Datasource> GetByIdAsync(DatasourceId datasourceId, CancellationToken cancellationToken);
        Task<Datasource> AddAsync(Datasource datasource, CancellationToken cancellationToken);
        
        //Task<bool> ExistsByNameAsync(DatasourceName name, CancellationToken cancellationToken);
        Task<List<Datasource>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Datasource datasource, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Datasource datasource, CancellationToken cancellationToken);
    }
}