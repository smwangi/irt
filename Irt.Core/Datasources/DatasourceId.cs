using Irt.Core.SeedWork;

namespace Irt.Core.Datasources
{
    public sealed class DatasourceId(string value) : TypedIdValueBase(value)
    {
        public string Value { get; } = value;
    }
}