using Irt.Core.SeedWork;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Configuration.DomainEvents;

public class DomainEventsDispatcher(IServiceProvider serviceProvider) : IDomainEventInterface.IDomainEventDispatcher
{
    public async Task DispatchEventsAsync(IEntity[] entities, CancellationToken cancellationToken = default)
    {
        if (entities == null || entities.Length == 0)
        {
            return;
        }

        var domainEvents = entities
            .SelectMany(entities => entities.DomainEvents)
            .ToList();
        
        if (!domainEvents.Any())
        {
            return;
        }
        
        // Clear domain events after dispatching
        foreach (var entity in entities)
        {
            entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await DispatchEvent(domainEvent, cancellationToken);
        }
    }

    private async Task DispatchEvent(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // Get the generic handler type for this specific event type
        var handlerType = typeof(IDomainEventInterface.IDomainEventHandler<>)
            .MakeGenericType(domainEvent.GetType());
        
        // Resolve the handler from the service provider
        var handlers = serviceProvider.GetServices(handlerType);
        
        // find handleAsync method
        var handleMethod = handlerType.GetMethod("HandleAsync");
        
        // execute each handler
        foreach (var handler in handlers)
        {
            if (handleMethod == null)
            {
                throw new InvalidOperationException($"HandleAsync method not found for {handlerType.Name}");
            }
            
            // Call the HandleAsync method on the handler
            await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken });
        }
    }
}