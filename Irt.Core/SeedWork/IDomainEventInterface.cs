namespace Irt.Core.SeedWork;

public interface IDomainEventInterface
{
    /// <summary>
    /// Interface for dispatching domain events
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches all domain events from the provided entities
        /// </summary>
        Task DispatchEventsAsync(IEntity[] entities, CancellationToken cancellationToken = default);
    }
    
    /// <summary>
    /// Interface for handling specific domain events
    /// </summary>
    /// <typeparam name="TEvent">The type of domain event to handle</typeparam>
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the specified domain event
        /// </summary>
        Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
    }
}