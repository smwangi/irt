using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Queries;

public sealed record GetIndicatorMainCategoryQuery(string? Search = null)
    : IQuery<Result<List<IndicatorMainCategoryDto>>>;

public sealed record GetIndicatorMainCategoryByIdQuery(string Id)
    : IQuery<Result<IndicatorMainCategoryDto>>;
