
namespace Irt.Core.SeedWork
{   
    public interface IDomainEvent
    {
        DateTime OccurredOn  => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName!;
        string EventId => Guid.NewGuid().ToString();
    }
}