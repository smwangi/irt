using Irt.Core.SeedWork;

namespace Irt.Core.Datasources.Events
{
    public record DatasourceUpdatedEvent : IDomainEvent
    {
        public DatasourceUpdatedEvent(Datasource datasource)
        {
            Datasource = datasource;
        }

        public Datasource Datasource { get; }
    }
}