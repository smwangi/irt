namespace Irt.UnitTest.Application.Mappers;

using AutoMapper;
using Irt.Application.Datasets;
using Irt.Core.Datasets;

public class DatasetMappingTests
{
    private readonly IMapper _mapper;

    public DatasetMappingTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Dataset, DatasetDto>()
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Description) ? "N/A" : src.Description));
        });

        configuration.AssertConfigurationIsValid(); // validate the configuration at startup
        _mapper = configuration.CreateMapper(); //create the mapper
    }

    [Fact]
    public void MappingConfiguration_ShouldBeValid()
    {
        // This test verifies that the mapping configuration is valid
        // This test verifies that the mapping configuration is valid
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Dataset, DatasetDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Description) ? "N/A" : src.Description));
        });

        // This will throw if there are issues
        configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void ShouldMap_DatasetToDatasetDto_Correctly()
    {
        // Arrange
        var dataset = TestData.CreateDataset();

        // Act
        var result = _mapper.Map<DatasetDto>(dataset);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dataset.Description, result.Description);
    }

    [Fact]
    public void ShouldMap_DatasetToDatasetDto_WhenDescriptionIsProvided()
    {
        // Arrange
        var dataset = TestData.CreateDataset(description: "Sample Description");

        // Act
        var result = _mapper.Map<DatasetDto>(dataset);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Sample Description", result.Description);
    }
}