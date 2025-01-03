using Irt.Core.SeedWork;

namespace Irt.Core.Datasets
{
    public sealed class DatasetId(string value) : TypedIdValueBase(value)
    {
        public string Value { get; } = value;
    }
}