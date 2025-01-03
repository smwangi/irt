using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects
{
    public class CreatedBy : ValueObject
    {
        public string Value { get; }
        public string UserId { get; }

        public CreatedBy(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new System.ArgumentException("Value cannot be null or whitespace.", nameof(userId));

            UserId = userId;
        }

        public static implicit operator string(CreatedBy createdBy) => createdBy is not null ? createdBy.Value : string.Empty;

        public static implicit operator CreatedBy(string value) => new CreatedBy(value);

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}