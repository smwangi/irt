using System.Linq.Expressions;
using Irt.Application.Common;
using Irt.Core.IndicatorDefinitions;

namespace Irt.Application.IndicatorDefinitions.Queries;

public class IndicatorDefinitionToDtoProjection
    : IProjection<IndicatorDefinition, IndicatorDefinitionDto>
{
    public Expression<Func<IndicatorDefinition, IndicatorDefinitionDto>> Expression =>
        IndicatorDefinitionDto.Projection;
}
