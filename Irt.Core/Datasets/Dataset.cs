using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.Datasets;

public sealed class Dataset : Entity<DatasetId>
{
    public string? Description { get; private set; }
    public Datasource Datasource { get; private set; }
    public DatasetType DatasetType { get; private set; }
    public IndicatorDefinition IndicatorDefinition { get; private set; }
    public string? SpreadJson { get; private set; } = string.Empty;
    public string? Bindings { get; private set; } = string.Empty;
    public string? Schedule { get; private set; } = string.Empty;
    public string? CronExpression { get; private set; } = string.Empty;
    public string? RelatedIndicatorsIds { get; private set; } = string.Empty;
    
    // For migration
    private Dataset() : base(null)
    {
        
    }
    private Dataset(
        DatasetId id,
        Name name,
        string description,
        Datasource source,
        DatasetType datasetType,
        IndicatorDefinition indicatorDefinition)
    {
        Description = description;
        Name = name;
        Id = id;
        Datasource = source;
        DatasetType = datasetType;
        IndicatorDefinition = indicatorDefinition;
        //this.AddDomainEvent(new Events.DatasetCreatedEvent(id));
    }

    public static Dataset CreateDataset(
        Name name,
        string description,
        Datasource source,
        DatasetType datasetType,
        IndicatorDefinition indicatorDefinition)
    {   
        return new Dataset(
            new DatasetId(UniqueIdGenerator.NextId()),
            name,
            description,
            source,
            datasetType,
            indicatorDefinition);
    }

    public Dataset WithUpdatedDataset(
        Name name,
        string description,
        Datasource datasource,
        IndicatorDefinition indicatorDefinition,
        DatasetType datasetType)
    {
        return new Dataset(
            id: Id,
            name: name,
            description: description,
            source: datasource,
            datasetType: datasetType,
            indicatorDefinition: indicatorDefinition
        );
    }
}
