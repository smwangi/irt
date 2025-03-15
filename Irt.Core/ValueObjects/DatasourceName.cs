
using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects
{
    public class DatasourceName: ValueObject
    {
        public string Value { get; }
        private const int MaxLength = 100;
        private const int MinLength = 4;

        private DatasourceName(string value) => Value = value;

        public static DatasourceName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            
            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new ArgumentException($"The length of the {nameof(DatasourceName)} must be between {MinLength} and {MaxLength} characters.");
            }

            return new DatasourceName(value);
        }

        public static implicit operator string(DatasourceName datasourceName)
        {
            return datasourceName.Value;
        }

        public static implicit operator DatasourceName(string value)
        {
            return new DatasourceName(value);
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}