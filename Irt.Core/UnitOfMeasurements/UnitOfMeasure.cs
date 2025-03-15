using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasure : Entity<UnitOfMeasureId>
    {
        public string Description { get; private set; }

        private UnitOfMeasure(
            Name name,
            string description,
            UnitOfMeasureId id) : base(id)
        {
            Name = name;
            Description = description;
            Id = id;
        }

        public static UnitOfMeasure CreateUnitOfMeasure(
            Name name,
            string description)
        {
            return new UnitOfMeasure(
                name, 
                description,
                new UnitOfMeasureId(UniqueIdGenerator.NextId()));
        }

        public void UpdateUnitOfMeasure(
            string name,
            string description)
        {
            Name = Name.Of(name);
            Description = description;
        }

        public void DeleteUnitOfMeasure()
        {
            //IsDeleted = true;
        }

        public void ApproveUnitOfMeasure()
        {
            //IsApproved = true;
        }
    }
}