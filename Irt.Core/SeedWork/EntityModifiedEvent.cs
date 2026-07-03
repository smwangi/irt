namespace Irt.Core.SeedWork;

public class EntityModifiedEvent : IDomainEvent
{
    public string EntityId { get; }
    public string EntityType { get; }
    public string UserId { get; }
    public string UserName { get; }
    public string Application { get; }
    public DateTime Timestamp { get; }
    
    public EntityModifiedEvent(string entityId, string entityType, string userId, 
        string userName, string application)
    {
        EntityId = entityId;
        EntityType = entityType;
        UserId = userId;
        UserName = userName;
        Application = application;
        Timestamp = DateTime.UtcNow;
    }
}