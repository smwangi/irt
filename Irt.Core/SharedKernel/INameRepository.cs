namespace Irt.Core.SharedKernel
{
    public interface INameRepository<TEntity>
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken, string? excludeEntityId = null);
    }
}