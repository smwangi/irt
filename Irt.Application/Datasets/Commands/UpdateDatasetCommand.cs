using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasets.Commands;
public class UpdateDatasetCommand(DatasetDto datasetDto) : CommandBase<Result<DatasetDto,string>>
{
    public DatasetDto DatasetDto { get; } = datasetDto ?? throw new ArgumentNullException(nameof(datasetDto));
}
public class UpdateDatasetCommandValidator : AbstractValidator<UpdateDatasetCommand>
{
    public UpdateDatasetCommandValidator()
    {
        RuleFor(x => x.DatasetDto).NotNull().WithMessage("DatasetDto is required.");
        RuleFor(x => x.DatasetDto.Id).NotEmpty().WithMessage("Id is a required field.");
        RuleFor(x => x.DatasetDto.DatasourceId).NotEmpty().WithMessage("DatasourceId is a required field.");
        RuleFor(x => x.DatasetDto.IndicatorDefinitionId).NotEmpty().WithMessage("IndicatorDefinitionId is a required field.");
        RuleFor(x => x.DatasetDto.Name).NotEmpty().WithMessage("Name is a required field.");

    }
}


