using Irt.Core.SeedWork;

namespace Irt.Core.Datasources.Events
{
    public class DatasourceCreatedEvent : DomainEventBase
    {
        public DatasourceCreatedEvent(DatasourceId datasourceId)
        {
            DatasourceId = datasourceId;
        }

        public DatasourceId DatasourceId { get; }
    }
}