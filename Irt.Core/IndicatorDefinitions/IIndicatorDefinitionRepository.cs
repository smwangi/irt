namespace Irt.Core.IndicatorDefinitions
{
    public interface IIndicatorDefinitionRepository
    {
        Task<IndicatorDefinition> GetByIdAsync(IndicatorDefinitionId indicatorDefinitionId, CancellationToken cancellationToken);
    }
}