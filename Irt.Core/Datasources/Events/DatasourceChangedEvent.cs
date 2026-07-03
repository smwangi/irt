using Irt.Core.SeedWork;

namespace Irt.Core.Datasources.Events
{
    public class DatasourceChangedEvent : DomainEventBase
    {
        public DatasourceChangedEvent(DatasourceId datasourceId)
        {
            DatasourceId = datasourceId;
        }

        public DatasourceId DatasourceId { get; }
    }
}