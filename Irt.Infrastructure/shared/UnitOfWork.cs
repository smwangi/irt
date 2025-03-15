using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database.Postgres;
using Microsoft.EntityFrameworkCore.Storage;

namespace Irt.Infrastructure.Shared;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories;
    
    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (_repositories.TryGetValue(type, out var value)) return (IGenericRepository<T>)value;
        value = new GenericRepository<T>(_context);
        _repositories[type] = value;
        return (IGenericRepository<T>)value;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction?.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction?.RollbackAsync();
    }
}