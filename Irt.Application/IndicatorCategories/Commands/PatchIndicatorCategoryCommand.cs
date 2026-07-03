using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands;

public sealed record PatchIndicatorCategoryCommand(
    string Name,
    string Description,
    string IndicatorMainCategoryId)
    : ICommand<Result<IndicatorCategoryDto>>, IIndicatorCategoryUpsertCommand
{
    public string Id { get; init; } = default!;
}
