using Irt.Core.SeedWork;

namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasureId(string value) : TypedIdValueBase<UnitOfMeasureId>(value)
    {
        public string Value { get; } = value;
    }
}