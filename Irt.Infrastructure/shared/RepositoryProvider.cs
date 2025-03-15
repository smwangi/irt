using Irt.Application.Helpers;

namespace Irt.Infrastructure.Shared;

// Service Locator
public class RepositoryProvider
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public RepositoryProvider(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public T GetRepository<T>() where T : class
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = _repositoryFactory.CreateFactory<T>();
        }

        return (T)_repositories[type];
    }
}