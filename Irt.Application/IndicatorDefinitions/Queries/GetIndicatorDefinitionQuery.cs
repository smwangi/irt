using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Queries;

public sealed record GetIndicatorDefinitionQuery(string? Search = null)
    : IQuery<Result<List<IndicatorDefinitionDto>>>;

public sealed record GetIndicatorDefinitionByIdQuery(string Id)
    : IQuery<Result<IndicatorDefinitionDto>>;