namespace Irt.Core.SeedWork
{
    public abstract class Aggregate<TId>(TId id) : Entity<TId>(id), IAggregate<TId>
        where TId : TypedIdValueBase<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            var domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }
    }
}