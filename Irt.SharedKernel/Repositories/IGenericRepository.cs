namespace Irt.Core.SharedKernel;

public interface IGenericRepository<T> : IRepository<T> where T : class
{
}