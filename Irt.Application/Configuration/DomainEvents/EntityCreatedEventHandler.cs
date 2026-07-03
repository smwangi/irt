using Irt.Application.Helpers;
using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.Application.Configuration.DomainEvents;

public class EntityCreatedEventHandler(IEntityRepository entityRepository) : IDomainEventInterface.IDomainEventHandler<EntityCreatedEvent>
{
    public async Task HandleAsync(EntityCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Find the entity
        var entity = await entityRepository.FindEntityByIdAndType(domainEvent.EntityId, domainEvent.EntityType, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException($"Entity with ID {domainEvent.EntityId} and type {domainEvent.EntityType} not found.");
        }

        var createdBy = CreatedBy.Create(
            domainEvent.UserId,
            domainEvent.UserName,
            domainEvent.Application,
            domainEvent.Timestamp);
        
        // Set Audit information on the entity
        entity.SetCreatedByInfo(createdBy);
        
        // Create lastmodifiedby information initially
        var lastModifiedBy = LastModifiedBy.Create(
            domainEvent.UserId,
            domainEvent.UserName,
            domainEvent.Application,
            domainEvent.Timestamp);
        
        entity.SetLastModifiedByInfo(lastModifiedBy);
        
        await entityRepository.SaveChangesAsync(cancellationToken);
    }
}
