using Irt.Application.Helpers;
using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.Application.Configuration.DomainEvents;

public class EntityModifiedEventHandler(IEntityRepository entityRepository) : IDomainEventInterface.IDomainEventHandler<EntityModifiedEvent>
{
    public async Task HandleAsync(EntityModifiedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Find the entity
        var entity = await entityRepository.FindEntityByIdAndType(domainEvent.EntityId, domainEvent.EntityType, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException($"Entity with ID {domainEvent.EntityId} and type {domainEvent.EntityType} not found.");
        }

        var lastModifiedBy = LastModifiedBy.Create(
            domainEvent.UserId,
            domainEvent.UserName,
            domainEvent.Application,
            domainEvent.Timestamp);

        // Set last modified by information on the entity
        entity.SetLastModifiedByInfo(lastModifiedBy);

        await entityRepository.SaveChangesAsync(cancellationToken);
    }
}
