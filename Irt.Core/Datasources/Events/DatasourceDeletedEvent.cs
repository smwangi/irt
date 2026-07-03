using Irt.Core.SeedWork;

namespace Irt.Core.Datasources.Events
{
    public class DatasourceDeletedEvent : DomainEventBase
    {
        public DatasourceDeletedEvent(DatasourceId datasourceId)
        {
            DatasourceId = datasourceId;
        }

        public DatasourceId DatasourceId { get; }
    }
}