namespace Irt.Core.SeedWork
{
    /// <summary>
    /// Interface for entities that can raise domain events.
    /// </summary>
    public interface IHasDomainEvents
    {
        /// <summary>
        /// Domain events occurred during the entity lifetime.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Clear domain events
        /// </summary>
        void ClearDomainEvents();
    }
}