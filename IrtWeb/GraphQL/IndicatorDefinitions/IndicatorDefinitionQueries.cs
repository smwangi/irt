using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.IndicatorDefinitions;
using Irt.Core.IndicatorDefinitions;
using Irt.Infrastructure.Database.Postgres;
using Microsoft.EntityFrameworkCore;

namespace IrtWeb.GraphQL.IndicatorDefinitions;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class IndicatorDefinitionQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<IndicatorDefinitionDto> GetIndicatorDefinitions(
        [Service] ApplicationDbContext db,
        [Service] IMapper mapper,
        string? search = null)
    {
        var query = db.IndicatorDefinitions
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{EscapeLikePattern(search.Trim())}%";

            query = query.Where(x => EF.Functions.ILike(x.Name.Value, pattern, "\\"));
        }

        return query.ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider);
    }

    [UseProjection]
    public IQueryable<IndicatorDefinitionDto> GetIndicatorDefinitionById(
        [Service] ApplicationDbContext db,
        [Service] IMapper mapper,
        string id)
    {
        var indicatorDefinitionId = IndicatorDefinitionId.Create(id);

        return db.IndicatorDefinitions
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.Id == indicatorDefinitionId)
            .ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider);
    }

    private static string EscapeLikePattern(string value) =>
        value
            .Replace("\\", "\\\\")
            .Replace("%", "\\%")
            .Replace("_", "\\_");
}
