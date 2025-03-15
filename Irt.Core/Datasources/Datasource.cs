using System.Text.Json.Serialization;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.Datasources
{
    public sealed class Datasource : Entity<DatasourceId> //, IAggregateRoot
    {

        public string? Description { get; private set; } = string.Empty;

        public string? Source { get; private set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DatasourceType DatasourceType { get; private set; }

        // for migration
        private Datasource()
        {
        }
        private Datasource(
            DatasourceId id,
            Name name,
            string description,
            string? source,
            DatasourceType datasourceType) : base(id)
        {
            Description = description;
            Id = id;
            Name = name;
            Source = source;
            DatasourceType = datasourceType;
            //this.AddDomainEvent(new Events.DatasourceCreatedEvent(id));
        }

        public static Datasource CreateDatasource(
            Name name,
            string description,
            string source,
            DatasourceType datasourceType)
        {
            return new Datasource(
                DatasourceId.Create(UniqueIdGenerator.NextId()),
                name,
                description,
                source,
                datasourceType
            );
        }

        public void UpdateDatasource(
            Name name,
            string description,
            DatasourceId datasourceId)
        {
            Name = name;
            Description = description;

            AddDomainEvent(new Events.DatasourceUpdatedEvent(this));
        }
    }
}