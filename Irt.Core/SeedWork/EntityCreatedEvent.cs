namespace Irt.Core.SeedWork;

public class EntityCreatedEvent : IDomainEvent
{
    public string EntityId { get; }
    public string EntityType { get; }
    public string UserId { get; }
    public string UserName { get; }
    public string Application { get; }
    public DateTime Timestamp { get; }
    public string IpAddress { get; }
    
    public EntityCreatedEvent(string entityId, string entityType, string userId, 
        string userName, string application, string ipAddress)
    {
        EntityId = entityId;
        EntityType = entityType;
        UserId = userId;
        UserName = userName;
        Application = application;
        Timestamp = DateTime.UtcNow;
        IpAddress = ipAddress;
    }
}