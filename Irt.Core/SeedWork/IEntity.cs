using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork
{
    public interface IEntity<out TEntityId> : IEntity where TEntityId : TypedIdValueBase<TEntityId>
    {
        public TEntityId Id { get; }
    }

    public interface IEntity
    {
        public DateTime? CreatedAt { get; }
        public DateTime? LastModifiedAt { get; }
        public CreatedBy CreatedBy { get;}
        public LastModifiedBy LastModifiedBy { get; }
    }
}