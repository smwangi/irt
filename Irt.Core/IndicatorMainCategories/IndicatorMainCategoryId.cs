

using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategoryId : TypedIdValueBase<IndicatorMainCategoryId>
    {
        public string Value { get; }
        private IndicatorMainCategoryId(string value) : base(value)
        {
            Value = value;
        }
        
        private IndicatorMainCategoryId() : base(string.Empty)
        {
        }
        
        public static IndicatorMainCategoryId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("IndicatorMainCategoryId cannot be empty", nameof(value));
            }
            
            return new IndicatorMainCategoryId(value);
        }
    }
}