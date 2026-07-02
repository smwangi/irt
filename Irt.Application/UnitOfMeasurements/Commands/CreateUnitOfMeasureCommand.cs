using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands;

public sealed record CreateUnitOfMeasureCommand(string Name, string Description)
    : ICommand<Result<UnitOfMeasureDto>>, IUnitOfMeasureUpsertCommand;
