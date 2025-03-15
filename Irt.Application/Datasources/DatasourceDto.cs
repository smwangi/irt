using System.ComponentModel.DataAnnotations;

namespace Irt.Application.Datasources
{
    public record DatasourceDto
    (
        string Id,
        [Required]
        string Name,
        string Description,
        string Source,
        string DatasourceType
    );
}