namespace Irt.Core.SeedWork
{
    public interface IAggregate<T> : IAggregate, IEntity where T : TypedIdValueBase<T>
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}