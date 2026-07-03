using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasure : NamedMetadataEntity<UnitOfMeasureId>
    {
        private UnitOfMeasure()
        {
        }

        private UnitOfMeasure(
            Name name,
            string description,
            UnitOfMeasureId id)
            : base(id, name, description)
        {
        }

        public static UnitOfMeasure Create(
            Name name,
            string description)
            => CreateUnitOfMeasure(name, description);

        public static UnitOfMeasure CreateUnitOfMeasure(
            Name name,
            string description)
        {
            return new UnitOfMeasure(
                name, 
                description,
                UnitOfMeasureId.Create(UniqueIdGenerator.NextId()));
        }

        public void UpdateUnitOfMeasure(
            string name,
            string description)
        {
            Update(name, description);
        }

        public void DeleteUnitOfMeasure()
        {
            MarkAsDeleted();
        }

        public void ApproveUnitOfMeasure()
        {
            //IsApproved = true;
        }
    }
}
