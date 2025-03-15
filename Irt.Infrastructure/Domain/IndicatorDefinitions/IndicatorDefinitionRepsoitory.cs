using System.Linq.Expressions;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database;
using Irt.Infrastructure.Shared;

namespace Irt.Infrastructure.Domain.IndicatorDefinitions;
public class IndicatorDefinitionRepository 
{
    //private readonly IMongoCollection<IndicatorDefinition> _indicatorDefinitions = irtDbContext.GetCollection<IndicatorDefinition>(CollectionNames.IndicatorDefinitions);
    public Task<IndicatorDefinition?> GetByIdAsync(IndicatorDefinitionId indicatorDefinitionId)
    {
        return null;
    }

    public Task<PaginationResult<IndicatorDefinition>> GetPaginatedAsync(int page, int pageSize)
    {
        return null;
    }

    public Task<List<IndicatorDefinition>> GetAllAsync(string query)
    {
        throw new NotImplementedException();
    }

    public  Task<IndicatorDefinition> AddAsync(IndicatorDefinition entity, CancellationToken cancellationToken)
    {
        return null;
    }

    public Task<IndicatorDefinition> UpdateAsync(IndicatorDefinition entity)
    {
        return null;
    }

    public Task<bool> DeleteAsync(IndicatorDefinition entity)
    {
        return null;
    }

    public Task<bool> ExistsAsync(Expression<Func<IndicatorDefinition, bool>> predicate)
    {
        //var filter = Builders<IndicatorDefinition>.Filter.Where(predicate);
        return Task.FromResult(false);
    }

    public Task<IndicatorDefinition?> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
