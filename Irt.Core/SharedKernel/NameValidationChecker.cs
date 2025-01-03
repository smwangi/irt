using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;

namespace Irt.Core.SharedKernel
{
    public class NameValidationChecker<T>: IBusinessRule
    {
        private readonly string _name;
        private const int MinLength = 2;

        private readonly int _maxLength;
        private readonly INameValidationChecker<T> _nameUniquenessChecker;

        public  NameValidationChecker(string name, int maxLength, INameValidationChecker<T> nameUniquenessChecker)
        {
            _name = name;
            _maxLength = maxLength;
            _nameUniquenessChecker = nameUniquenessChecker;
        }

        public string Message => "Name must be unique.";

        private async Task<bool> IsNameNotUnique()
        {
            return await _nameUniquenessChecker.IsNameUniqueAsync(Name.Of(_name), CancellationToken.None);
        }

        private Task<bool> IsNameWithinRange() => Task.FromResult(_name.Length >= MinLength || _name.Length <= _maxLength);

        public async Task<bool> IsBroken()
        {
            return await IsNameNotUnique() || await IsNameWithinRange();
        }
    }
}