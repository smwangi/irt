namespace Irt.Core.SharedKernel;

public interface IRepositoryUniversal : IDisposable
{
    Task<T?> GetByIdAsync<T>(string id) where T : class;
    Task CreateAsync<T>(T entity) where T : class;
    Task UpdateAsync<T>(string id, T entity) where T : class;
    Task DeleteAsync(string id);
    Task<IEnumerable<T>> QueryAsync<T>(string n1qlQuery, object parameters = null);
}