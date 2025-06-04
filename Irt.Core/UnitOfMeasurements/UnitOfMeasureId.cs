using Irt.Core.SeedWork;

namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasureId: TypedIdValueBase<UnitOfMeasureId>
    {
        public string Value { get; }

        private UnitOfMeasureId() : base(string.Empty)
        {
        }
        
        private UnitOfMeasureId(string value) : base(value)
        {
            Value = value;
        }
        
        public static UnitOfMeasureId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("UnitOfMeasureId cannot be empty", nameof(value));
            }
            
            return new UnitOfMeasureId(value);
        }
    }
}