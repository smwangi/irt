using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using MongoDB.Bson;

namespace Irt.Core.UnitOfMeasurements
{
    public class UnitOfMeasure : Entity<UnitOfMeasureId>
    {
        public string Description { get; private set; }

        private UnitOfMeasure(
            Name name,
            string description,
            UnitOfMeasureId id)
        {
            Name = name;
            Description = description;
            Id = id;
        }

        public static UnitOfMeasure CreateUnitOfMeasure(
            Name name,
            string description,
            INameValidationChecker<UnitOfMeasure> nameUniqueChecker)
        {
            CheckRule(new NameUniquenessChecker<UnitOfMeasure>(nameUniqueChecker, name, null));
            return new UnitOfMeasure(
                name,
                description,
                new UnitOfMeasureId(ObjectId.GenerateNewId().ToString()));
        }

        public void UpdateUnitOfMeasure(
            Name name,
            string description)
        {
            Name = name;
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