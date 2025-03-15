using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;

namespace Irt.Core.SharedKernel;

public interface IUniquenessChecker
{
    Task<bool> IsNameUniqueAsync<TEntity>(Name name, CancellationToken cancellationToken) where TEntity : class, IEntity;
    Task<bool> IsNameUniqueAsync<TEntity, TId>(Name name, TId excludeId, CancellationToken cancellationToken)
        where TId : TypedIdValueBase<TId>
        where TEntity : class, IEntity;
}