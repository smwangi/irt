using Irt.SharedKernel.Repositories;

namespace Irt.SharedKernel.Repositories;

public interface IGenericRepository<T> : IRepository<T> where T : class
{
}