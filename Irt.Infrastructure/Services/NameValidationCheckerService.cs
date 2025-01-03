using System.Runtime.CompilerServices;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Irt.Infrastructure.Database;
using MongoDB.Driver;

namespace Irt.Infrastructure.Services
{
    public class NameValidationCheckerService<TEntity> : INameValidationChecker<TEntity>
    {
        private readonly INameRepository<TEntity> _nameRepository;

        public NameValidationCheckerService(INameRepository<TEntity> nameRepository)
        {
            _nameRepository = nameRepository;
        }
        //private readonly IMongoCollection<T> mongoCollection = new M
        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken, string? excludeEntityId = null)
        {
            return await _nameRepository.ExistsAsync(name, cancellationToken, excludeEntityId);
        }

        public Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken, string? id = null)
        {
            return _nameRepository.ExistsAsync(name.Value, cancellationToken, id);
        }

        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}