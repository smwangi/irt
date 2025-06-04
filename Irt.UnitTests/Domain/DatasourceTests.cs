using Irt.Core.Datasources;
using Irt.Core.ValueObjects;
using Xunit;

namespace Irt.UnitTests.Domain
{
    public class DatasourceTests()
    {
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
            var updatedDatasourceName = Name.Of("UpdatedDatasourceName") ;
            var updatedDatasourceDescription = "UpdatedDatasourceDescription";
            datasource.WithUpdatedDatasource(
                updatedDatasourceName,
                updatedDatasourceDescription);

            // Assert
            Assert.Equal(updatedDatasourceName.Value, datasource.Name?.Value);
            Assert.Equal(updatedDatasourceDescription, datasource.Description);
        }

        private Datasource CreateTestDatasource()
        {
            return Datasource.CreateDatasource(
                Name.Of("DatasourceName"),
                "DatasourceDescription",
                source: "unep", DatasourceType.Csv);
        }
    }
}