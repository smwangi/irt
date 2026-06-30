using System.Linq.Expressions;
using Irt.Application.Common;
using Irt.Core.ReportingScopes;

namespace Irt.Application.ReportingScopes.Queries;

public class ReportingScopeToDtoProjection : IProjection<ReportingScope, ReportingScopeDto>
{
    public Expression<Func<ReportingScope, ReportingScopeDto>> Expression => entity => new ReportingScopeDto
    {
        Id = entity.Id.Value,
        Name = entity.Name.Value,
        Description = entity.Description
    };
}