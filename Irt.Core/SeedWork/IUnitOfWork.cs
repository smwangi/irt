using System;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.Repositories;

namespace Irt.Core.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}