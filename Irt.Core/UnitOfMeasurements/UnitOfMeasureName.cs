using Irt.Core.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasureName : ValueObject
    {
        private const int MaxLength = 100;
        private const int MinLength = 2;

        [BsonElement("value")]
        public string Value { get; }

        [BsonConstructor]
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
        
        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}