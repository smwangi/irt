using System.Runtime.CompilerServices;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;

namespace Irt.Core.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        private const int MaxLength = 100;
        private const int MinLength = 2;

        private Name(string value)
        {
            Value = value;
        }

        public static Name Of(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new System.ArgumentException("Name cannot be empty", nameof(value));
            }

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new System.ArgumentException($"The length of the name must be between {MinLength} and {MaxLength} characters.", nameof(value));
            }

            return new Name(value);
        }
    }
}