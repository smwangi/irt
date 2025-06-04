using Irt.Core.SharedKernel;
using Irt.SharedKernel.Repositories;

namespace Irt.Core.IndicatorDefinitions
{
    public interface IIndicatorDefinitionRepository : IRepository<IndicatorDefinition>
    {
        Task<IndicatorDefinition?> GetByIdAsync(IndicatorDefinitionId indicatorDefinitionId,
            CancellationToken cancellationToken);
    }
}