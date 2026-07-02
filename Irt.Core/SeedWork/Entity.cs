using System.ComponentModel.DataAnnotations.Schema;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork;
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity<TId> : IHasDomainEvents, IEntity, ISoftDeletable where TId : TypedIdValueBase<TId>
    {
        [NotMapped]
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Domain events occurred during the entity lifetime.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public TId Id { get; protected set; }
        public Name Name { get; protected set; }
        public CreatedBy CreatedBy { get; private set; }
        public LastModifiedBy LastModifiedBy { get; private set; }

        public bool IsApproved { get; private set; } = false;
        public bool IsDeleted { get; private set; } = false;

        protected Entity()
        {
            
        }
        protected Entity(TId id)
        {
            Id = id;
            // Register Creation with Default user
            RegisterCreation("system", "system", "system", "127.0.0.1");
        }
        
        protected Entity(TId id, Name name)
        {
            Id = id;
            Name = name;
            // Register Creation with Default user
            RegisterCreation("system", "system", "system", "127.0.0.1");
        }
        
        // Overload that allows specifying creation details
        protected Entity(TId id, string userId, string userName, string application, string ipAddress)
        {
            Id = id;
            RegisterCreation(userId, userName, application, ipAddress);
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear domain events
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Sets the created by information directly (for event handlers and infrastructure)
        /// This should not be called from domain logic, only from event handlers
        /// </summary>
        public void SetCreatedByInfo(CreatedBy createdBy)
        {
            CreatedBy = createdBy;
        }

        /// <summary>
        /// Sets the last modified information directly (for event handlers and infrastructure)
        /// This should not be called from domain logic, only from event handlers
        /// </summary>
        public void SetLastModifiedByInfo(LastModifiedBy lastModifiedBy)
        {
            LastModifiedBy = lastModifiedBy;
        }
        /// <summary>
        /// Registers that this entity has been created with the specified user information.
        /// Raises an EntityCreatedEvent that will be handled to set audit information.
        /// </summary>
        public void RegisterCreation(
            string userId,
            string userName,
            string application,
            string ipAddress)
        {
            CreatedBy = CreatedBy.Create(
                userId, 
                userName, 
                application, 
                ipAddress
            );
            var entityType = this.GetType().Name;
            AddDomainEvent(new EntityCreatedEvent(
                Id.Value, 
                entityType, 
                userId, 
                userName, 
                application,
                ipAddress
            ));
        }

        /// <summary>
        /// Registers that this entity has been modified with the specified user information.
        /// Raises an EntityModifiedEvent that will be handled to update audit information.
        /// </summary>
        public void RegisterModification(
            string userId,
            string userName,
            string application,
            string ipAddress)
        {
            LastModifiedBy = LastModifiedBy.Create(
                userId, 
                userName, 
                application, 
                ipAddress
            );
            var entityType = this.GetType().Name;
            AddDomainEvent(new EntityModifiedEvent(
                Id.Value, 
                entityType, 
                userId, 
                userName, 
                application
            ));
        }
        
        public void MarkAsDeleted() => IsDeleted = true;
    }