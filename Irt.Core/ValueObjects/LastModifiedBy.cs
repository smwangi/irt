using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects
{
    public class LastModifiedBy : ValueObject
    {
        public string Value { get; }

        private LastModifiedBy()
        {
        }
        public LastModifiedBy(string value)
        {
            Value = value;
        }

        public static implicit operator string(LastModifiedBy lastModifiedBy) => lastModifiedBy is not null ? lastModifiedBy.Value : string.Empty;

        public static implicit operator LastModifiedBy(string value) => new LastModifiedBy(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}