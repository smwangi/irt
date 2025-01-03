using Irt.Core.Datasources;
using Irt.Core.Exceptions;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Irt.Core.Datasets
{
    public sealed class Dataset : Entity<DatasetId>
    {
        public string? Description { get; private set; } = string.Empty;
        public Datasource Datasource { get; private set; }
        public DatasetType DatasetType { get; private set; }
        public IndicatorDefinition IndicatorDefinition { get; private set; }
        public string? SpreadJson { get; private set; } = string.Empty;
        public string? Bindings { get; private set; } = string.Empty;
        public string? Schedule { get; private set; } = string.Empty;
        public string? CronExpression { get; private set; } = string.Empty;
        public string? RelatedIndicatorsIds { get; private set; } = string.Empty;

        [BsonConstructor]
        private Dataset(
            DatasetId id,
            Name name,
            string description,
            Datasource source,
            DatasetType datasetType,
            IndicatorDefinition indicatorDefinition)
        {
            Name = name;
            Description = description;
            Id = id;
            this.Datasource = source;
            this.DatasetType = datasetType;
            this.IndicatorDefinition = indicatorDefinition;
            //this.AddDomainEvent(new Events.DatasetCreatedEvent(id));
        }

        public static Dataset CreateDataset(
            Name name,
            string description,
            Datasource source,
            DatasetType datasetType,
            IndicatorDefinition indicatorDefinition,
            INameValidationChecker<Dataset> datasetUniqueChecker)
        {   
            CheckRule(new NameUniquenessChecker<Dataset>(datasetUniqueChecker, name, null));
            return new Dataset(
                new DatasetId(ObjectId.GenerateNewId().ToString()),
                name,
                description,
                source,
                datasetType,
                indicatorDefinition);
        }

        public void UpdateDataset(
            string name,
            string description,
            DatasetId datasetId,
            Datasource datasource,
            IndicatorDefinition indicatorDefinition,
            DatasetType datasetType,
            INameValidationChecker<Dataset> datasetUniqueChecker)
        {
            Name datasetName =  Name.Of(name);
            CheckRule(new NameUniquenessChecker<Dataset>(datasetUniqueChecker, datasetName, datasetId.Id));
            Name = datasetName;
            Description = description;
            Datasource = datasource;
            IndicatorDefinition = indicatorDefinition;
            DatasetType = datasetType;
        }

    }
}