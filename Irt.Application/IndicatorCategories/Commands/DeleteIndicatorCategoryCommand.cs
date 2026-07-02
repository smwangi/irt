using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands;

public sealed record DeleteIndicatorCategoryCommand(string Id) : ICommand<Result<Unit>>;
