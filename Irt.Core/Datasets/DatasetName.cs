using Irt.Core.SeedWork;

namespace Irt.Core.Datasets
{
    public sealed class DatasetName : ValueObject
    {
        private const int MaxLength = 100;
        private const int MinLength = 3;

        public string Value { get; }

        private DatasetName(string value)
        {
            Value = value;
        }

        public static DatasetName Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The dataset name cannot be null or empty.");
            }

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new ArgumentException($"The length of the dataset name must be between {MinLength} and {MaxLength} characters.");
            }

            return new DatasetName(value);
        }

        public static implicit operator string(DatasetName datasetName)
        {
            return datasetName.Value;
        }

        public static implicit operator DatasetName(string value)
        {
            return new DatasetName(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}