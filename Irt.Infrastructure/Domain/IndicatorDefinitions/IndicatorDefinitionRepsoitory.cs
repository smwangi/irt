using Irt.Core.IndicatorDefinitions;
using Irt.Infrastructure.Database;

namespace Irt.Infrastructure.Domain.IndicatorDefinitions;
public class IndicatorDefinitionRepository(IrtDbContext irtDbContext) : IIndicatorDefinitionRepository
{
    public Task<IndicatorDefinition> GetByIdAsync(IndicatorDefinitionId indicatorDefinitionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
