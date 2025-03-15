using System.ComponentModel.DataAnnotations.Schema;
using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork;
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity<TId> : IHasDomainEvents, IEntity where TId : TypedIdValueBase<TId>
    {
        [NotMapped]
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Domain events occurred during the entity lifetime.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public TId Id { get; protected init; }
        public Name Name { get; protected set; }
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; private set; }
        public CreatedBy CreatedBy { get; private set; }
        public LastModifiedBy LastModifiedBy { get; private set; }

        public bool IsApproved { get; private set; } = false;
        public bool IsDeleted { get; private set; } = false;

        private Entity()
        {
            
        }
        protected Entity(TId id)
        {
            Id = id;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear domain events
        /// </summary>
        public static async Task CheckRuleAsync(IBusinessRule rule)
        {
            if (await rule.IsBrokenAsync())
                throw new BusinessRuleValidationException(rule);
        }

        public static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBrokenAsync().Result)
                throw new BusinessRuleValidationException(rule);
        }

        public bool IsTransient()
        {
            return false;// this.Id == default;
        }

        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }

        public void SetCreated()
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = new CreatedBy("system");
            SetModified();
        }

        internal void SetModified()
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = new LastModifiedBy("system");
        }
    
    }

    public class Entity2
    {
        public Entity2(Name name)
        {
            //Name = name;
        }
        
    }