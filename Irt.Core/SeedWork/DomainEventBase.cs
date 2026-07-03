using System;

namespace Irt.Core.SeedWork
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public DateTime OccurredOn { get; }

        protected DomainEventBase()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }
}