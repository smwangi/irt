using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Commands;

public sealed record DeleteUnitOfMeasureCommand(string Id) : ICommand<Result<Unit>>;
