using System.Text.Json.Serialization;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasources
{
    public class CreateDatasourceCommand(
        string name,
        string description,
        string source,
        string datasourceType) : CommandBase<Result<DatasourceDto, string>>
    {

        [JsonPropertyName("description")]
        public string Description { get; } = description;

        [JsonPropertyName("name")]
        public string Name { get; } = name;
        public string Source { get; } = source;
        public string DatasourceType { get; } = datasourceType;
    }
}