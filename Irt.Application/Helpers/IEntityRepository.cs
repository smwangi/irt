using Irt.Core.SeedWork;

namespace Irt.Application.Helpers;

public interface IEntityRepository
{
    // <summary>
    /// Finds an entity by its ID and type name
    /// </summary>
    Task<IEntity> FindEntityByIdAndType(string id, string typeName, CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Saves changes to the repository
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}