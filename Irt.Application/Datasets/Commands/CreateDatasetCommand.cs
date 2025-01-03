using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasets
{
    public class CreateDatasetCommand(
        string name,
        string description,
        string datasourceId,
        string indicatorDefinitionId,
        string datasetType) : CommandBase<Result<DatasetDto, string>>
    {
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string DatasourceId { get; } = datasourceId;

        public string IndicatorDefinitionId { get; } = indicatorDefinitionId;
        public DatasetType DatasetType { get; } = Enum.Parse<DatasetType>(datasetType);
    }
}