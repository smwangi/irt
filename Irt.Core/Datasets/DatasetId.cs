using Irt.Core.SeedWork;

namespace Irt.Core.Datasets
{
    public sealed class DatasetId: TypedIdValueBase<DatasetId>
    {
        public DatasetId(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("DatasetId cannot be empty", nameof(value));

            Value = value;
        }

        // For migration
        private DatasetId() : base(string.Empty)
        {
        }
        
        public string Value { get; }
    }
}