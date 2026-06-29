using System.Linq.Expressions;
using Irt.Core.ReportingScopes;

namespace Irt.Application.ReportingScopes;

public record ReportingScopeDto(
    string Id,
    string Name,
    string Description
)
{
    public static Expression<Func<ReportingScope, ReportingScopeDto>> Projection { get; } =
        scope => new ReportingScopeDto(
            scope.Id,
            scope.Name.Value,
            scope.Description
        );
}