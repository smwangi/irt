using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork
{
    public interface IEntity<TEntityId> where TEntityId : TypedIdValueBase<TEntityId>
    {
        TEntityId Id { get; }
        Name Name { get; }
    }

    public interface IEntity
    {
        // <summary>
        /// Sets the created by information directly (for event handlers and infrastructure)
        /// </summary>
        void SetCreatedByInfo(CreatedBy createdBy);
        
        /// <summary>
        /// Sets the last modified information directly (for event handlers and infrastructure)
        /// </summary>
        void SetLastModifiedByInfo(LastModifiedBy lastModifiedBy);
        bool IsApproved { get; }
        bool IsDeleted { get; }
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}