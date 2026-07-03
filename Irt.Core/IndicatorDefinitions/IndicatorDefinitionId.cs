using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorDefinitions
{
    public sealed class IndicatorDefinitionId : TypedIdValueBase<IndicatorDefinitionId>
    {
        public string Value { get; }

        private IndicatorDefinitionId(string value)
            : base(value: value)
        {
            this.Value = value;
        }
        
        private IndicatorDefinitionId() : base(string.Empty)
        {
        }
        
        public static IndicatorDefinitionId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("IndicatorDefinitionId cannot be empty", nameof(value));
            }
            
            return new IndicatorDefinitionId(value);
        }
    }
}