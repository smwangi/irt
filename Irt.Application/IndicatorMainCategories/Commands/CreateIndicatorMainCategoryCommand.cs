using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands;

public sealed record CreateIndicatorMainCategoryCommand(string Name, string Description)
    : ICommand<Result<IndicatorMainCategoryDto>>, IIndicatorMainCategoryUpsertCommand;
