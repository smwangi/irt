using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Queries;

public sealed record GetIndicatorCategoryQuery(
    string? Search = null,
    string? IndicatorMainCategoryId = null)
    : IQuery<Result<List<IndicatorCategoryDto>>>;

public sealed record GetIndicatorCategoryByIdQuery(string Id)
    : IQuery<Result<IndicatorCategoryDto>>;
