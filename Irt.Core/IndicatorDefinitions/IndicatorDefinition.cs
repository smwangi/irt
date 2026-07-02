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
            Name = name ?? Name;
            Description = description ?? Description;
            ReportingScope = reportingScope ?? ReportingScope;
            UnitOfMeasure = unitOfMeasure ?? UnitOfMeasure;
            IndicatorCategory = indicatorCategory ?? IndicatorCategory;

            var effectiveMin = minThreshold ?? MinThreshold;
            var effectiveMax = maxThreshold ?? MaxThreshold;
            EnsureThresholds(effectiveMin, effectiveMax);
            MinThreshold = effectiveMin;
            MaxThreshold = effectiveMax;

            Formula = formula ?? Formula;
            FormulaDescription = formulaDescription ?? FormulaDescription;
            Metadata = metadata ?? Metadata;
            DPSIR = dpsir ?? DPSIR;
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