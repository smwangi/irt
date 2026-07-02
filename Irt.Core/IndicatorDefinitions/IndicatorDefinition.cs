using Irt.Core.IndicatorCategories;
using Irt.Core.ReportingScopes;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
namespace Irt.Core.IndicatorDefinitions
{
    public class IndicatorDefinition : Entity<IndicatorDefinitionId>
    {
        public string Description { get; private set; }
        public ReportingScope ReportingScope { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; private set; }
        public IndicatorCategory IndicatorCategory { get; private set; }
        public decimal MinThreshold { get; private set; }
        public decimal MaxThreshold { get; private set; }
        public string? Formula { get; private set; }
        public string? FormulaDescription { get; private set; }
        public string? Metadata { get; private set; }
        public string? DPSIR { get; private set; }

        private IndicatorDefinition()
        {
        }

        private IndicatorDefinition(
            IndicatorDefinitionId id,
            Name name,
            string description,
            ReportingScope reportingScope,
            UnitOfMeasure unitOfMeasure,
            IndicatorCategory indicatorCategory,
            decimal minThreshold,
            decimal maxThreshold,
            string? formula,
            string? formulaDescription,
            string? metadata,
            string? dpsir)
        {
            Id = id;
            Name = name;
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
            string? formula,
            string? formulaDescription,
            string? metadata,
            string? dpsir)
        {
            EnsureThresholds(minThreshold, maxThreshold);
            EnsureReferences(reportingScope, unitOfMeasure, indicatorCategory);

            return new IndicatorDefinition(
                IndicatorDefinitionId.Create(UniqueIdGenerator.NextId()),
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

        public void Update(
            Name name,
            string description,
            ReportingScope reportingScope,
            UnitOfMeasure unitOfMeasure,
            IndicatorCategory indicatorCategory,
            decimal minThreshold,
            decimal maxThreshold,
            string? formula,
            string? formulaDescription,
            string? metadata,
            string? dpsir)
        {
            EnsureThresholds(minThreshold, maxThreshold);
            EnsureReferences(reportingScope, unitOfMeasure, indicatorCategory);

            Name = name;
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

        public void Patch(
            Name? name,
            string? description,
            ReportingScope? reportingScope,
            UnitOfMeasure? unitOfMeasure,
            IndicatorCategory? indicatorCategory,
            decimal? minThreshold,
            decimal? maxThreshold,
            string? formula,
            string? formulaDescription,
            string? metadata,
            string? dpsir)
        {
            if (name is not null) Name = name;
            if (description is not null) Description = description;
            if (reportingScope is not null) ReportingScope = reportingScope;
            if (unitOfMeasure is not null) UnitOfMeasure = unitOfMeasure;
            if (indicatorCategory is not null) IndicatorCategory = indicatorCategory;

            var effectiveMin = minThreshold ?? MinThreshold;
            var effectiveMax = maxThreshold ?? MaxThreshold;
            EnsureThresholds(effectiveMin, effectiveMax);
            MinThreshold = effectiveMin;
            MaxThreshold = effectiveMax;

            if (formula is not null) Formula = formula;
            if (formulaDescription is not null) FormulaDescription = formulaDescription;
            if (metadata is not null) Metadata = metadata;
            if (dpsir is not null) DPSIR = dpsir;
        }

        private static void EnsureThresholds(decimal min, decimal max)
        {
            if (min > max)
            {
                throw new ArgumentException(
                    $"MinThreshold ({min}) cannot be greater than MaxThreshold ({max}).");
            }
        }

        private static void EnsureReferences(
            ReportingScope reportingScope,
            UnitOfMeasure unitOfMeasure,
            IndicatorCategory indicatorCategory)
        {
            ArgumentNullException.ThrowIfNull(reportingScope);
            ArgumentNullException.ThrowIfNull(unitOfMeasure);
            ArgumentNullException.ThrowIfNull(indicatorCategory);
        }
    }
}