
using Irt.Core.SharedKernel;
using Moq;

namespace Irt.UnitTest.Domain
{
    public class DatasourceTest
    {
        private readonly INameValidationChecker<Datasource> _nameValidationChecker = new Mock<INameValidationChecker<Datasource>>().Object;

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
            var updatedDatasourceName = "UpdatedDatasourceName";
            var updatedDatasourceDescription = "UpdatedDatasourceDescription";
            datasource.UpdateDatasource(updatedDatasourceName, updatedDatasourceDescription, datasource.Id, _nameValidationChecker);

            // Assert
            Assert.Equal(updatedDatasourceName, datasource.Name.Value);
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
            var nameValidationChecker = _nameValidationChecker;
            var expectedMessage = $"The length of the name must be between {MinLength} and {MaxLength} characters. (Parameter 'value')";

            // Act
            //var testDelegate = () => Datasource.CreateDatasource(name, description, source, datasourceType, nameValidationChecker);
            Datasource testDelegate() => Datasource.CreateDatasource(name, description, source, datasourceType, nameValidationChecker);

            // Assert
            var ex = Assert.Throws<ArgumentException>((Func<Datasource>)testDelegate);
            Assert.Equal(expectedMessage, ex.Message);

        }

        private Datasource CreateTestDatasource()
        {
            return Datasource.CreateDatasource(
                $"{Guid.NewGuid()}-DatasourceName",
                "DatasourceDescription",
                source: "unep",
                DatasourceType.Csv,
                _nameValidationChecker);
        }
    }
}