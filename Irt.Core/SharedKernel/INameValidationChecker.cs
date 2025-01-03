using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;

namespace Irt.Core.SharedKernel
{
    public interface INameValidationChecker<TEntity>
    {
         Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken, string? id = null);
        //Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
    }
}