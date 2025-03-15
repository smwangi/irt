using Irt.Core.SeedWork;

namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasureName : ValueObject
    {
        private const int MaxLength = 100;
        private const int MinLength = 2;

        public string Value { get; }

        private UnitOfMeasureName(string value) => Value = value;

        public static UnitOfMeasureName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new ArgumentException($"The length of the {nameof(UnitOfMeasureName)} must be between {MinLength} and {MaxLength} characters.");
            }

            return new UnitOfMeasureName(value);
        }

        public static implicit operator string(UnitOfMeasureName unitOfMeasureName)
        {
            return unitOfMeasureName.Value;
        }

        public static implicit operator UnitOfMeasureName(string value)
        {
            return new UnitOfMeasureName(value);
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