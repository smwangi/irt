using Irt.Core.Datasets;
using Irt.Infrastructure.Database;
using Irt.Infrastructure.Shared;
using Irt.Core.Datasources;
using MongoDB.Driver;

namespace Irt.Infrastructure.Datasets;
public class DatasetRepository(IrtDbContext dbContext) : Repository<Dataset>(dbContext, CollectionNames.Datasets), IDatasetRepository
{
    private IMongoCollection<Dataset> _datasets = dbContext.GetCollection<Dataset>(CollectionNames.Datasets);
    public async Task<Dataset> AddAsync(Dataset dataset, CancellationToken cancellationToken)
    {
        await _datasets.InsertOneAsync(dataset, cancellationToken: cancellationToken);

        return dataset;
    }

    public Task<bool> DeleteAsync(Dataset dataset, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Eq(d => d.Id, dataset.Id);
        return _datasets.DeleteOneAsync(filter, cancellationToken: cancellationToken).ContinueWith(t => t.Result.DeletedCount > 0);
    }

    public Task<List<Dataset>> GetAllAsync(CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Empty;
        return _datasets.Find(filter).ToListAsync(cancellationToken);
    }

    /**
    * Pagination Logic:

        Skip((pageNumber - 1) * pageSize): Skips the rows of previous pages.
        Take(pageSize): Limits the result to the current page size.
        Ordering:

        Use OrderBy to ensure consistent ordering. Without it, the results may not be deterministic.
        Validation:

        Ensure pageNumber and pageSize have valid values.
        Performance:

        Use indexes in the database for better performance, especially if you're filtering or ordering by specific fields.
    */

    public Task<List<Dataset>> GetByDatasourceIdAndTypeAsync(DatasourceId datasourceId, DatasetType datasetType, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.And(
            Builders<Dataset>.Filter.Eq("Datasource.Id", datasourceId),
            Builders<Dataset>.Filter.Eq(d => d.DatasetType, datasetType)
        );
        return _datasets.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<List<Dataset>> GetByDatasourceIdAsync(DatasourceId datasourceId, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Eq("Datasource.Id", datasourceId);
        return _datasets.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<Dataset> GetByIdAsync(DatasetId datasetId, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Eq(d => d.Id, datasetId);
        return _datasets.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Dataset>> GetByTypeAsync(DatasetType datasetType, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Eq(d => d.DatasetType, datasetType);
        return _datasets.Find(filter).ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(Dataset dataset, CancellationToken cancellationToken)
    {
        var filter = Builders<Dataset>.Filter.Eq(d => d.Id, dataset.Id);
        return _datasets.ReplaceOneAsync(filter, dataset, cancellationToken: cancellationToken);
    }
}