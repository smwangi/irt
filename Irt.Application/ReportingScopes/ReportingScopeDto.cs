namespace Irt.Application.ReportingScopes;

public class ReportingScopeDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public ReportingScopeDto() { }

    public ReportingScopeDto(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
