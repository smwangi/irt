using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel;

public interface INameUniquenessChecker<TEntity, TId> : IUniquenessChecker where TEntity : class where TId :  TypedIdValueBase<TId>
{
    Task<bool> IsNameUniqueAsync(string nameValue, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string nameValue, TId excludeId, CancellationToken cancellationToken = default);
}