using Irt.Core.Datasets;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.ReportingScopes;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;

namespace Irt.UnitTest
{
    public class TestData
    { 
        //private static readonly NameUniquenessChecker<Dataset> NameUniquenessChecker = new NameUniquenessChecker<Dataset>(new NameUniquenessChecker<Dataset>(name, Name.Of("Dataset1"), null);
        /*public static IEnumerable<object[]> GetDatasetData()
        {
            yield return new object[]
            {
                "Dataset1",
                "Description1",
                new Datasource("Datasource1"),
                new DatasetType("DatasetType1"),
                new IndicatorDefinition(
                    new IndicatorDefinitionId(ObjectId.GenerateNewId().ToString()),
                    Name.Of("IndicatorDefinition1"),
                    "Description1",
                    new ReportingScopes("ReportingScope1"),
                    new UnitOfMeasure("UnitOfMeasure1"),
                    new IndicatorCategory("IndicatorCategory1"),
                    1,
                    2,
                    "Formula1",
                    "FormulaDescription1",
                    "Metadata1",
                    "DPSIR1"),
                new NameUniquenessChecker<Dataset>(new DatasetUniqueChecker(), Name.Of("Dataset1"), null)
            };
        }*/

        public static ReportingScope CreateReportingScope()
        {
            return ReportingScope.CreateReportingScope(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString()
            );
        }

        public static UnitOfMeasure CreateUnitOfMeasure()
        {
            return UnitOfMeasure.CreateUnitOfMeasure(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString()
            );
        }

        public static IndicatorCategory CreateIndicatorCategory()
        {
            return IndicatorCategory.CreateIndicatorCategory(
                name: Name.Of(Guid.NewGuid().ToString()), 
                description: Guid.NewGuid().ToString(),
                indicatorMainCategory: CreateIndicatorMainCategory()
            );
        }

        public static IndicatorMainCategory CreateIndicatorMainCategory()
        {
            return IndicatorMainCategory.Create(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString()
            );
        }
        public static IndicatorDefinition CreateIndicatorDefinition()
        {
            return IndicatorDefinition.CreateIndicatorDefinition(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString(),
                reportingScope: CreateReportingScope(),
                unitOfMeasure: CreateUnitOfMeasure(),
                indicatorCategory: CreateIndicatorCategory(),
                0,
                100,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty);
        }
        public static Datasource CreateDatasource()
        {
            return Datasource.CreateDatasource(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString(),
                source: Guid.NewGuid().ToString(),
                datasourceType: DatasourceType.File
            );
        }

        public static Dataset CreateDataset(string? description = null)
        {
            return Dataset.CreateDataset(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: description ?? Guid.NewGuid().ToString(),
                source: CreateDatasource(),
                datasetType: DatasetType.Internal,
                indicatorDefinition: CreateIndicatorDefinition()
            );
        }

    }
}