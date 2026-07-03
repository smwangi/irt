using Irt.Core.SeedWork;

namespace Irt.Core.Datasources
{
    public sealed class DatasourceId : TypedIdValueBase<DatasourceId>
    {
        public string Value { get; }
        private DatasourceId(string value) : base(value)
        {
            Value = value;
        }

        private DatasourceId(): base("")
        {
            
        }

        public static DatasourceId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("DatasourceId cannot be empty", nameof(value));
            }
            
            return new DatasourceId(value);
        }

        public override string ToString() => Value;
    }
}