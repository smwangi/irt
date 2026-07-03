using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Queries;

public sealed record GetUnitOfMeasureQuery(string? Search = null)
    : IQuery<Result<List<UnitOfMeasureDto>>>;

public sealed record GetUnitOfMeasureByIdQuery(string Id)
    : IQuery<Result<UnitOfMeasureDto>>;
