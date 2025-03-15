using Irt.Core.Datasets;
using Irt.Core.SharedKernel;
using Moq;

namespace Irt.UnitTest.Domain
{
    public class DatasetTest
    {
        private Dataset dataset1 = TestData.CreateDataset();

        [Fact]
        public void TestCreateDataset_Succeeds()
        {
            // Arrange
            
                
            var dataset = TestData.CreateDataset();

            // Act & Assert
            Assert.NotNull(dataset);
            //Assert.NotNull(dataset.Datasource);
            //Assert.NotNull(dataset.Id);

        }

        [Fact]
        public void TestUpdateDataset_Succeeds()
        {
            // Arrange
            var dataset = dataset1;

            // Act
            var updatedName = "new-dataset-name";
            var updatedDescription = Guid.NewGuid().ToString();
            dataset.UpdateDataset(
                updatedName,
                updatedDescription,
                null,
                datasource: TestData.CreateDatasource(),
                indicatorDefinition: TestData.CreateIndicatorDefinition(),
                datasetType: DatasetType.Internal);

            // Assert
            //Assert.Equal(updatedName, dataset.Name.Value);
            Assert.Equal(updatedDescription, dataset.Description);
        }

        [Fact]
        public void UpdateDataset_ThrowsException_WhenNameIsEmpty()
        {
            // Arrange
            var dataset = dataset1;
            var updatedName = string.Empty;
            var updatedDescription = string.Empty;

            // Act
            //Action act = () => dataset.UpdateDataset(updatedName, updatedDescription, dataset.Id, nameValidationChecker);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => dataset.UpdateDataset(
                updatedName,
                updatedDescription,
                null,
                datasource: TestData.CreateDatasource(),
                indicatorDefinition: TestData.CreateIndicatorDefinition(),
                datasetType: DatasetType.Internal));
        }
    }
}