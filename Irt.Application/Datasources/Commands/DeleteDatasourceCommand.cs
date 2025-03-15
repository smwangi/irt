using System.Windows.Input;
using FluentValidation;
using Irt.Application.Configuration.Commands;

namespace Irt.Application.Datasources.Commands
{
    public class DeleteDatasourceCommand(string datasourceId)
                : CommandBase<DeleteDatasourceResult>
    {
        public string DatasourceId { get; } = datasourceId;
    }

    public record DeleteDatasourceResult(bool IsSuccess);

    public class DeleteDatasourceCommandValidator : AbstractValidator<DeleteDatasourceCommand>
    {
        public DeleteDatasourceCommandValidator()
        {
            RuleFor(x => x.DatasourceId).NotEmpty().WithMessage("DatasourceId is required");
        }
    }
}