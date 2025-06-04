using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Queries;

public class GetIndicatorDefinitionQuery : IQuery<Result<List<IndicatorDefinitionDto>>>
{
    
}

public class GetIndicatorDefinitionByIdQuery(string id) : IQuery<Result<IndicatorDefinitionDto>>
{
    
}