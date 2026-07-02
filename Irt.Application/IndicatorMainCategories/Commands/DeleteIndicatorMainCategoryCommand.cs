using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands;

public sealed record DeleteIndicatorMainCategoryCommand(string Id) : ICommand<Result<Unit>>;
