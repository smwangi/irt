using System.Data;
using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;

namespace Irt.Application.Datasets.Commands
{
    public class CreateDatasetCommand(
        DatasetDto request) : CommandBase<Result<DatasetDto, string>>
    {
        public DatasetDto Request { get; } = request ?? throw new ArgumentNullException(nameof(request));
        public DatasetType DatasetType { get; } = Enum.Parse<DatasetType>(request.DatasetType.ToString());
    }

    public class CreateDatasetCommandValidator : AbstractValidator<CreateDatasetCommand>
    {
        public CreateDatasetCommandValidator()
        {
            RuleFor(x => x.Request).NotNull().WithMessage("Request is required.");
            RuleFor(x => x.Request.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Request.DatasourceId).NotEmpty().WithMessage("DatasourceId is required.");
            RuleFor(x => x.Request.IndicatorDefinitionId).NotEmpty().WithMessage("IndicatorDefinitionId is required.");
            RuleFor(x => x.Request.DatasetType).IsInEnum().WithMessage("DatasetType is required.");
        }
    }
}