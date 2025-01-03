using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity<T> : IHasDomainEvents, IEntity<T> where T : TypedIdValueBase
    {
        [NotMapped]
        private readonly List<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Domain events occurred during the entity lifetime.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        [Key]
        public T Id { get; protected set; }
        public Name Name { get; protected set; } = default!;
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; private set; }
        public CreatedBy CreatedBy { get; private set; }
        public LastModifiedBy LastModifiedBy { get; private set; }

        public bool IsApproved { get; private set; } = false;
        public bool IsDeleted { get; private set; } = false;

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }        

        /// <summary>
        /// Clear domain events
        /// </summary>
        public static async Task CheckRuleAsync(IBusinessRule rule)
        {
            if (await rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }

        public static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken().Result)
                throw new BusinessRuleValidationException(rule);
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        /*public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;
            
            if (GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            if (a.Id == default || b.Id == default)
                return false;

            return a.Id == b.Id;
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }*/

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
}