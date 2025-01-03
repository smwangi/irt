
using Irt.Core.SeedWork;
using Irt.Infrastructure.Database;
using Irt.Infrastructure.Processing;

namespace Irt.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly IrtDbContext _context;
        private IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(IrtDbContext context, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await Task.FromResult(1) ;//_context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}