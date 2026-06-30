using Irt.Application.ReportingScopes;
using Irt.Core.ReportingScopes;
using Irt.Infrastructure.Database.Postgres;
using Microsoft.EntityFrameworkCore;

namespace IrtWeb.GraphQL.ReportingScopes;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class ReportingScopeQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<ReportingScopeDto> GetReportingScopes(
        [Service] ApplicationDbContext db,
        string? search = null)
    {
        IQueryable<ReportingScope> query;

        if (string.IsNullOrWhiteSpace(search))
        {
            query = db.ReportingScopes
                .AsNoTracking()
                .Where(x => !x.IsDeleted);
        }
        else
        {
            var pattern = $"%{EscapeLikePattern(search.Trim())}%";

            query = db.ReportingScopes
                .FromSqlInterpolated($@"SELECT *
FROM irt.reporting_scopes
WHERE ""IsDeleted"" = FALSE
AND ""Name"" ILIKE {pattern} ESCAPE '\'")
                .AsNoTracking();
        }

        return query
            .Select(ReportingScopeDto.Projection);
    }
    
    [UseProjection]
    public IQueryable<ReportingScopeDto> GetReportingScopeById(
        [Service] ApplicationDbContext db, string id)
    {
        var reportingScopeId = ReportingScopeId.Create(id);

        return db.ReportingScopes
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.Id == reportingScopeId)
            .Select(ReportingScopeDto.Projection);
    }

    private static string EscapeLikePattern(string value) =>
        value
            .Replace("\\", "\\\\")
            .Replace("%", "\\%")
            .Replace("_", "\\_");
}
