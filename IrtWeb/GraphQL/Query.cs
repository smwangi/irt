using HotChocolate;
using HotChocolate.Types;
using Irt.Application.ReportingScopes;
using Irt.Infrastructure.Database.Postgres;
using Microsoft.EntityFrameworkCore;

namespace IrtWeb.GraphQL;

[QueryType]
public sealed class Query
{
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<ReportingScopeDto> GetReportingScopes(
        [Service] ApplicationDbContext db) =>
        db.ReportingScopes
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .Select(ReportingScopeDto.Projection);
}