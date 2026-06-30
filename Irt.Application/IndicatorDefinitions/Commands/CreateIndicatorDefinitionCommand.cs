using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands;

public record CreateIndicatorDefinitionCommand : ICommand<Result<IndicatorDefinitionDto>>
{
    public string Name { get; init; }
    public string Description { get; init; }
}