using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands;

public sealed record UpdateUnitOfMeasureCommand(string Name, string Description)
    : ICommand<Result<UnitOfMeasureDto>>, IUnitOfMeasureUpsertCommand
{
    public string Id { get; init; } = default!;
}
