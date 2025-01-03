using Irt.Core.Datasources;
using Irt.Core.SharedKernel;
using Xunit;

namespace Irt.UnitTests.Domain
{
    public class DatasourceTests
    {
        private readonly INameValidationChecker<Datasource> _datasourceUniqueChecker;
        public DatasourceTests(INameValidationChecker<Datasource> datasourceUniqueChecker)
        {
            _datasourceUniqueChecker = datasourceUniqueChecker;
        }

        [Fact]
        public void CreateDatasource()
        {
            // Arrange
            var datasource = CreateTestDatasource();

            // Act
            var createdDatasource = datasource;

            // Assert
            Assert.NotNull(createdDatasource);
            Assert.NotNull(createdDatasource.Id);
            Assert.Equal("DatasourceName", createdDatasource.Name.Value);
            Assert.Equal("DatasourceDescription", createdDatasource.Description);
            Assert.Equal(DatasourceType.Csv, createdDatasource.DatasourceType);
        }

        [Fact]
        public void UpdateDatasource()
        {
            // Arrange
            var datasource = CreateTestDatasource();

            // Act
            var updatedDatasourceName = "UpdatedDatasourceName";
            var updatedDatasourceDescription = "UpdatedDatasourceDescription";
            datasource.UpdateDatasource(updatedDatasourceName, updatedDatasourceDescription, datasource.Id, _datasourceUniqueChecker);

            // Assert
            Assert.Equal(updatedDatasourceName, datasource.Name?.Value);
            Assert.Equal(updatedDatasourceDescription, datasource.Description);
        }

        private Datasource CreateTestDatasource()
        {
            return Datasource.CreateDatasource(
                "DatasourceName", "DatasourceDescription", source: "unep", DatasourceType.Csv, _datasourceUniqueChecker);
        }
    }
}