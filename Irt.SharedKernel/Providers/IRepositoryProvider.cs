
using Irt.SharedKernel.Repositories;

namespace Irt.SharedKernel.Providers;

public interface IRepositoryProvider
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}