using Irt.Core.Datasources;
using Irt.Core.SharedKernel;

namespace Irt.Core.Datasets
{
    public interface IDatasetRepository : IRepository<Dataset>
    {
        Task<Dataset> GetByIdAsync(DatasetId datasetId, CancellationToken cancellationToken);
        Task<List<Dataset>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Dataset>> GetByDatasourceIdAsync(DatasourceId datasourceId, CancellationToken cancellationToken);
        Task<List<Dataset>> GetByTypeAsync(DatasetType datasetType, CancellationToken cancellationToken);
        Task<List<Dataset>> GetByDatasourceIdAndTypeAsync(DatasourceId datasourceId, DatasetType datasetType, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Dataset dataset, CancellationToken cancellationToken);
    }
}