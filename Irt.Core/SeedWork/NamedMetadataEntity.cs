using Irt.Core.ValueObjects;

namespace Irt.Core.SeedWork;

public abstract class NamedMetadataEntity<TId> : Entity<TId>
    where TId : TypedIdValueBase<TId>
{
    public string Description { get; protected set; } = string.Empty;

    protected NamedMetadataEntity()
    {
    }

    protected NamedMetadataEntity(TId id, Name name, string description)
        : base(id, name)
    {
        Description = description;
    }

    public void Update(string name, string description)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        Name = Name.Of(name);
    }

    public void SetDescription(string description)
    {
        Description = description;
    }
}
