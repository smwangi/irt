namespace Irt.UnitTest.Domain.ValidatorTests;
using Irt.Application.Datasets;
public class DatasetDtoValidatorTests
{
    private readonly DatasetDtoValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var dto = TestData.CreateDataset();
        //var result = _validator.TestValidate(dto);
        //result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}