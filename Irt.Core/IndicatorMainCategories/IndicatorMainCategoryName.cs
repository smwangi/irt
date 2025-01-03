using Irt.Core.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategoryName : ValueObject
    {
        private const int MaxLength = 100;
        private const int MinLength = 4;
        [BsonElement("value")]
        public string Value { get; private set; }

        private IndicatorMainCategoryName(string value) => Value = value;

        public static IndicatorMainCategoryName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new ArgumentException($"The length of the {nameof(IndicatorMainCategoryName)} must be between {MinLength} and {MaxLength} characters.");
            }

            return new IndicatorMainCategoryName(value);
        }
        public static implicit operator string(IndicatorMainCategoryName name)
        {
            return name.Value;
        }

        public static implicit operator IndicatorMainCategoryName(string name)
        {
            return new IndicatorMainCategoryName(name);
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
}