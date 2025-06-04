using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class UniquenessChecker(ApplicationDbContext applicationDbContext) :  IUniquenessChecker
{
    public async Task<bool> IsNameUniqueAsync<TEntity>(Name name, CancellationToken cancellationToken) where TEntity : class, IEntity
    {
        return !await applicationDbContext.Set<TEntity>()
            .AnyAsync(e => e..Name.Value == name.Value, cancellationToken);
    }

    public Task<bool> IsNameUniqueAsync<TEntity, TId>(Name name, TId excludeId, CancellationToken cancellationToken) where TEntity : class, IEntity where TId : TypedIdValueBase<TId>
    {
        return applicationDbContext.Set<TEntity>()
            .AnyAsync(e => e.Name.Value == name.Value && e.I != excludeId, cancellationToken);
    }
}