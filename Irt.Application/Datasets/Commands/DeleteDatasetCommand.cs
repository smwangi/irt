using FluentValidation;
using Irt.Application.Configuration.Commands;

namespace Irt.Application.Datasets.Commands
{
    public class DeleteDatasetCommand(string datasetId) : CommandBase<DeleteDatasetResult>
    {
        public string DatasetId { get; } = datasetId;
    }

    public record DeleteDatasetResult(bool IsSuccess);

    public class DeleteDatasetCommandValidator : AbstractValidator<DeleteDatasetCommand>
    {
        public DeleteDatasetCommandValidator()
        {
            RuleFor(x => x.DatasetId).NotEmpty().WithMessage("DatasetId is required");
        }
    }
}