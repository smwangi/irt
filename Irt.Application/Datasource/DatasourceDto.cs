using System.ComponentModel.DataAnnotations;

namespace Irt.Application.Datasource
{
    public record DatasourceDto
    (
        string Id,
        string Name,
        string Description,
        string Source,
        string DatasourceType
    );
}