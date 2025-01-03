using Irt.Core.IndicatorCategories;
using Irt.Core.ReportingScopes;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
using MongoDB.Bson;

namespace Irt.Core.IndicatorDefinitions
{
    public class IndicatorDefinition : Entity<IndicatorDefinitionId>
    {
        public string Description { get; private set; }
        public Name IndicatorDefinitionName { get; private set; }
        public ReportingScope ReportingScope { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; private set; }
        public IndicatorCategory IndicatorCategory { get; private set; }
        public decimal MinThreshold { get; private set; }
        public decimal MaxThreshold { get; private set; }
        public string? Formula { get; private set; }
        public string? FormulaDescription { get; private set; }
        public string? Metadata { get; private set; }
        public string? DPSIR { get; private set; }

        private IndicatorDefinition(
            IndicatorDefinitionId id,
            Name name,
            string description,
            ReportingScope reportingScope,
            UnitOfMeasure unitOfMeasure,
            IndicatorCategory indicatorCategory,
            decimal minThreshold,
            decimal maxThreshold,
            string formula,
            string formulaDescription,
            string metadata,
            string dpsir)
        {
            Id = id;
            IndicatorDefinitionName = name;
            Description = description;
            ReportingScope = reportingScope;
            UnitOfMeasure = unitOfMeasure;
            IndicatorCategory = indicatorCategory;
            MinThreshold = minThreshold;
            MaxThreshold = maxThreshold;
            Formula = formula;
            FormulaDescription = formulaDescription;
            Metadata = metadata;
            DPSIR = dpsir;
        }

        public static IndicatorDefinition CreateIndicatorDefinition(
            Name name,
            string description,
            ReportingScope reportingScope,
            UnitOfMeasure unitOfMeasure,
            IndicatorCategory indicatorCategory,
            decimal minThreshold,
            decimal maxThreshold,
            string formula,
            string formulaDescription,
            string metadata,
            string dpsir,
            INameValidationChecker<IndicatorDefinition> nameValidationChecker)
        {
            Name indicatorName = Name.Of(name.Value);
            CheckRule(new NameUniquenessChecker<IndicatorDefinition>(nameValidationChecker, indicatorName));
            return new IndicatorDefinition(
                new IndicatorDefinitionId(ObjectId.GenerateNewId().ToString()),
                name,
                description,
                reportingScope,
                unitOfMeasure,
                indicatorCategory,
                minThreshold,
                maxThreshold,
                formula,
                formulaDescription,
                metadata,
                dpsir
            );
        }
    }
}