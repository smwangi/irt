using Irt.Core.Datasets;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Application.Datasets;

public class DatasetNameValidationChecker : INameValidationChecker<Dataset>
{
    private readonly IDatasetRepository _datasetRepository;

    public DatasetNameValidationChecker(IDatasetRepository datasetRepository)
    {
        _datasetRepository = datasetRepository;
    }

    public async Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken, string? id = null)
    {
        //var dataset = await _datasetRepository.GetByNameAsync(name, cancellationToken);
        return true; //dataset == null || dataset.Id == id;
    }
}