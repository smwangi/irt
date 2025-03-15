using Irt.Core.SharedKernel;

namespace Irt.Core.IndicatorDefinitions
{
    public interface IIndicatorDefinitionRepository : IRepository<IndicatorDefinition>
    {
        Task<IndicatorDefinition?> GetByIdAsync(IndicatorDefinitionId indicatorDefinitionId,
            CancellationToken cancellationToken);
    }
}