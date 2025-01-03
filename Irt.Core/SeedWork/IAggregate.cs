namespace Irt.Core.SeedWork
{
    public interface IAggregate<T> : IAggregate, IEntity<T> where T : TypedIdValueBase
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}