using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Commands;

public record UpdateDatasetCommand(
    string Id,
    string Name,
    string Description,
    string DatasourceId,
    string DatasetType,
    string IndicatorDefinitionId,
    string? UserId,
    string? Username,
    string? Application,
    string? IpAddress) : ICommand<Result<DatasetDto>>;
public class UpdateDatasetCommandValidator : AbstractValidator<UpdateDatasetCommand>
{
    public UpdateDatasetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is a required field.");
        RuleFor(x => x.DatasourceId).NotEmpty().WithMessage("DatasourceId is a required field.");
        RuleFor(x => x.IndicatorDefinitionId).NotEmpty().WithMessage("IndicatorDefinitionId is a required field.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is a required field.");
        RuleFor(x => x.DatasetType).NotEmpty().WithMessage("DatasetType is a required field.");
    }
}


