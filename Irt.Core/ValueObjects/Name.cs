using System.Runtime.CompilerServices;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;

namespace Irt.Core.ValueObjects;

    public class Name : ValueObject
    {
        public string Value { get; }

        private const int MaxLength = 100;
        private const int MinLength = 2;

        private Name(){}
        private Name(string value)
        {
            Value = value;
        }

        public static Name Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty", nameof(value));
            }

            if (value.Length is < MinLength or > MaxLength)
            {
                throw new ArgumentException($"The length of the name must be between {MinLength} and {MaxLength} characters.", nameof(value));
            }

            return new Name(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
