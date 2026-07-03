using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands;

public sealed record PatchIndicatorMainCategoryCommand(string Name, string Description)
    : ICommand<Result<IndicatorMainCategoryDto>>, IIndicatorMainCategoryUpsertCommand
{
    public string Id { get; init; } = default!;
}
