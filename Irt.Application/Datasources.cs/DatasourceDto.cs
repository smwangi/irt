namespace Irt.Application.Datasources
{
    public record DatasourceDto
    (
        string Id,
        string Name,
        string Description,
        string Source,
        string DatasourceType,
        string CreatedBy,
        DateTime? CreatedAt,
        string LastModifiedBy,
        DateTime? LastModifiedAt
    );
}