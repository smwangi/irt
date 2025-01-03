using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Irt.Core.Datasources
{
    [BsonIgnoreExtraElements]
    public sealed class Datasource : Entity<DatasourceId> //, IAggregateRoot
    {
        //[Required]
        //public new Name Name { get; private set; }

        public string? Description { get; private set; } = string.Empty;

        public string? Source { get; private set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public DatasourceType DatasourceType { get; private set; }

        [BsonConstructor]
        private Datasource(
            DatasourceId id,
            Name name,
            string description,
            string? source,
            DatasourceType datasourceType)
        {
            Name = name;
            Description = description;
            Id = id;
            Source = source;
            DatasourceType = datasourceType;
            //this.AddDomainEvent(new Events.DatasourceCreatedEvent(id));
        }

        public static Datasource CreateDatasource(
            string name,
            string description,
            string source,
            DatasourceType datasourceType,
            INameValidationChecker<Datasource> nameUniqueChecker)
        {
            Name nam = Name.Of(name);
            CheckRule(new NameUniquenessChecker<Datasource>(nameUniqueChecker, nam, null));
            return new Datasource(
                new DatasourceId(ObjectId.GenerateNewId().ToString()),
                nam,
                description,
                source,
                datasourceType
            );
        }

        public void UpdateDatasource(
            string name,
            string description,
            DatasourceId datasourceId,
            INameValidationChecker<Datasource> nameUniqueChecker)
        {
            Name nam = Name.Of(name);
            CheckRule(new NameUniquenessChecker<Datasource>(nameUniqueChecker, nam, datasourceId.Id));
            //Validate(name, description, nameValidationChecker);
            Name = nam;
            Description = description;
            SetModified();

            AddDomainEvent(new Events.DatasourceUpdatedEvent(this));
        }

        /*private static async void Validate(DatasourceName name, string description, IDatasourceUniqueChecker datasourceUniqueChecker)
        {
            await CheckRuleAsync(new NameValidationChecker(datasourceUniqueChecker, name));
        }*/
    }
}