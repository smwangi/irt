using Irt.Application.Helpers;
using Irt.Core.Datasets;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Shared;

namespace Irt.Infrastructure.Domain.Datasets;

public class DatasetService(IRepositoryFactory repositoryFactory)
{
    private readonly IRepository<Dataset> _repository = repositoryFactory.CreateFactory<Dataset>();

    public async Task<IEnumerable<Dataset>> GetAllAsync()
    {
        var query = $"SELECT * FROM irt.irt.datasets";
        return await _repository.GetAllAsync(query:query, CancellationToken.None);
    }
}