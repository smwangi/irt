using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.Application.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Commands
{
    public record UpdateDatasourceCommand(
        string Id,
        string Name,
        string Description,
        string Source,
        string DatasourceType) : ICommand<Result<DatasourceDto>>;

    public class UpdateDatasourceCommandValidator : AbstractValidator<UpdateDatasourceCommand>
    {
        public UpdateDatasourceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
