using System.Linq.Expressions;
using Irt.Core.ReportingScopes;

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

    public static Expression<Func<ReportingScope, ReportingScopeDto>> Projection { get; } =
        scope => new ReportingScopeDto
        {
            Id = scope.Id.Value,
            Name = scope.Name.Value,
            Description = scope.Description
        };
}