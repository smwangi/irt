
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Moq;

namespace Irt.UnitTest.Domain
{
    public class DatasourceTest
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
            Assert.Equal("DatasourceDescription", createdDatasource.Description);
            Assert.Equal(DatasourceType.Csv, createdDatasource.DatasourceType);
        }

        [Fact]
        public void UpdateDatasource()
        {
            // Arrange
            var datasource = CreateTestDatasource();

            // Act
            var updatedDatasourceName = Name.Of("UpdatedDSName");
            var updatedDatasourceDescription = "UpdatedDatasourceDescription";
            datasource.UpdateDatasource(
                updatedDatasourceName,
                updatedDatasourceDescription);

            // Assert
            Assert.Equal(updatedDatasourceName.Value, datasource.Name.Value);
            Assert.Equal(updatedDatasourceDescription, datasource.Description);
        }

        [Fact]
        public void Throws_Exception_When_Name_IsLessThan_Min()
        {
            // Arrange
            var MinLength = 2;
            var MaxLength = 100;
            var name = "s";
            var description = "description";
            var source = "src";
            var datasourceType = DatasourceType.File;
            var expectedMessage = $"The length of the name must be between {MinLength} and {MaxLength} characters. (Parameter 'value')";

            // Act
            //var testDelegate = () => Datasource.CreateDatasource(name, description, source, datasourceType, nameValidationChecker);
            Datasource testDelegate() => Datasource.CreateDatasource(Name.Of(name), description, source, datasourceType);

            // Assert
            var ex = Assert.Throws<ArgumentException>((Func<Datasource>)testDelegate);
            Assert.Equal(expectedMessage, ex.Message);

        }

        private Datasource CreateTestDatasource()
        {
            return Datasource.CreateDatasource(
                Name.Of($"{Guid.NewGuid()}-DatasourceName"),
                "DatasourceDescription",
                source: "unep",
                DatasourceType.Csv);
        }
    }
}