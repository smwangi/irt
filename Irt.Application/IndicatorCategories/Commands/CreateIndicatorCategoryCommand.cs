using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands;

public sealed record CreateIndicatorCategoryCommand(
    string Name,
    string Description,
    string IndicatorMainCategoryId)
    : ICommand<Result<IndicatorCategoryDto>>, IIndicatorCategoryUpsertCommand;
