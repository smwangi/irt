using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;

namespace Irt.Core.SharedKernel
{
    public class NameUniquenessChecker<TEntity>(INameValidationChecker<TEntity> uniqueChecker, Name name, string? id = null) : IBusinessRule
    {
        private readonly INameValidationChecker<TEntity> _uniqueChecker = uniqueChecker;
        private readonly Name _name = name;

        public async Task<bool> IsBroken() => await _uniqueChecker.IsNameUniqueAsync(_name, CancellationToken.None, id);

        public string Message => $"{nameof(TEntity)} with this name already exists";
    }
}