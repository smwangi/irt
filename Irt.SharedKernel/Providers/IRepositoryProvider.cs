using Irt.Core.SharedKernel;

namespace Irt.Infrastructure.Shared;

public interface IRepositoryProvider
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}