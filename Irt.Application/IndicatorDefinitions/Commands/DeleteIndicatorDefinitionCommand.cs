using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands;

public sealed record DeleteIndicatorDefinitionCommand(string Id)
    : ICommand<Result<Unit>>;
