using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorDefinitions
{
    public class IndicatorDefinitionId(string value) : TypedIdValueBase(value)
    {
        public string Value { get; } = value;
    }
}