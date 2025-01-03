using Irt.Core.Datasources;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Application.Datasources;
public class DatasourceNameValidationChecker : INameValidationChecker<Datasource>
{
    private readonly IDatasourceRepository _datasourceRepository;

    public DatasourceNameValidationChecker(IDatasourceRepository datasourceRepository)
    {
        _datasourceRepository = datasourceRepository;
    }

    public async Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken, string? id = null)
    {
        //var datasource = await _datasourceRepository.GetByNameAsync(name, cancellationToken);
        return true; // datasource == null || datasource.Id == id;
    }
}