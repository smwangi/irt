using System.Windows.Input;
using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands
{
    public class DeleteDatasourceCommand(string datasourceId)
                : ICommand<Result<Unit>>
    {
        public string DatasourceId { get; } = datasourceId;
    }

    public class DeleteDatasourceCommandValidator : AbstractValidator<DeleteDatasourceCommand>
    {
        public DeleteDatasourceCommandValidator()
        {
            RuleFor(x => x.DatasourceId).NotEmpty().WithMessage("DatasourceId is required");
        }
    }
}