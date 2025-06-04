using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorCategories
{
    public sealed class IndicatorCategoryId: TypedIdValueBase<IndicatorCategoryId>
    {
        public string Value { get; }

        private IndicatorCategoryId(string value)
            : base(value)
        {
            this.Value = value;
        }
        
        private IndicatorCategoryId() : base(string.Empty) {}
        
        public static IndicatorCategoryId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("IndicatorCategoryId cannot be empty", nameof(value));
            }
            
            return new IndicatorCategoryId(value);
        }
    }
}