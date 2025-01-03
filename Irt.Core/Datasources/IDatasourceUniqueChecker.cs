namespace Irt.Core.Datasources
{
    public interface IDatasourceUniqueChecker
    {
        Task<bool> IsDatasourceNameUnique(string name);
    }
}