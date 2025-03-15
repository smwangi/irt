using Irt.Core.SeedWork;

namespace Irt.Core.Datasources
{
    public sealed class DatasourceId : TypedIdValueBase<DatasourceId>
    {
        public string Value { get; }
        public DatasourceId(string value) : base(value)
        {
            Value = value;
        }

        private DatasourceId(): base("")
        {
            
        }
    }
}