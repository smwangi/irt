using Irt.Core.Datasets;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.ReportingScopes;
using Irt.Core.SharedKernel;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
using Moq;

namespace Irt.UnitTest
{
    public class TestData
    {
        private readonly INameValidationChecker<Dataset> nameValidationChecker = new Mock<INameValidationChecker<Dataset>>().Object;
        private readonly INameValidationChecker<Datasource> datasourceNameValidation = new Mock<INameValidationChecker<Datasource>>().Object;
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
                    new ReportingScope("ReportingScope1"),
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
                description: Guid.NewGuid().ToString(),
                nameValidationChecker: GetMock<INameValidationChecker<ReportingScope>>()
            );
        }

        public static UnitOfMeasure CreateUnitOfMeasure()
        {
            return UnitOfMeasure.CreateUnitOfMeasure(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString(),
                nameUniqueChecker: GetMock<INameValidationChecker<UnitOfMeasure>>()
            );
        }

        public static IndicatorCategory CreateIndicatorCategory()
        {
            return IndicatorCategory.CreateIndicatorCategory(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString(),
                indicatorMainCategory: CreateIndicatorMainCategory(),
                nameValidationChecker: GetMock<INameValidationChecker<IndicatorCategory>>()
            );
        }

        public static IndicatorMainCategory CreateIndicatorMainCategory()
        {
            return IndicatorMainCategory.Create(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: Guid.NewGuid().ToString(),
                nameValidationChecker: GetMock<INameValidationChecker<IndicatorMainCategory>>()
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
                string.Empty,
                nameValidationChecker: GetMock<INameValidationChecker<IndicatorDefinition>>());
        }
        public static Datasource CreateDatasource()
        {
            return Datasource.CreateDatasource(
                name: Guid.NewGuid().ToString(),
                description: Guid.NewGuid().ToString(),
                source: Guid.NewGuid().ToString(),
                datasourceType: DatasourceType.File,
                nameUniqueChecker: GetMock<INameValidationChecker<Datasource>>()
            );
        }

        public static Dataset CreateDataset(string? description = null)
        {
            return Dataset.CreateDataset(
                name: Name.Of(Guid.NewGuid().ToString()),
                description: description ?? Guid.NewGuid().ToString(),
                source: CreateDatasource(),
                datasetType: DatasetType.Internal,
                indicatorDefinition: CreateIndicatorDefinition(),
                datasetUniqueChecker: GetNameValidationChecker<Dataset>()
            );
        }
        
        private static readonly Dictionary<Type, object> _mocks = new()
        {
            { typeof(INameValidationChecker<Dataset>), new Mock<INameValidationChecker<Dataset>>().Object },
            { typeof(INameValidationChecker<Datasource>), new Mock<INameValidationChecker<Datasource>>().Object },
            { typeof(INameValidationChecker<ReportingScope>), new Mock<INameValidationChecker<ReportingScope>>().Object },
            { typeof(INameValidationChecker<UnitOfMeasure>), new Mock<INameValidationChecker<UnitOfMeasure>>().Object },
            { typeof(INameValidationChecker<IndicatorCategory>), new Mock<INameValidationChecker<IndicatorCategory>>().Object },
            { typeof(INameValidationChecker<IndicatorMainCategory>), new Mock<INameValidationChecker<IndicatorMainCategory>>().Object },
            { typeof(INameValidationChecker<IndicatorDefinition>), new Mock<INameValidationChecker<IndicatorDefinition>>().Object }
        };

        private static T GetMock<T>()
        {
            return (T)_mocks[typeof(T)];
        }

        private static INameValidationChecker<T> GetNameValidationChecker<T>()
        {
            return (INameValidationChecker<T>)_mocks[typeof(INameValidationChecker<T>)];
        }
    }
}