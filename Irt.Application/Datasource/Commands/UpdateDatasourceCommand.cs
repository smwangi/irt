using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasources.Commands
{
    public class UpdateDatasourceCommand(DatasourceDto datasourceRequest) : CommandBase<Result<DatasourceDto>>
    {
        public DatasourceDto DatasourceRequest { get; } = datasourceRequest;
    }

    public class UpdateDatasourceCommandValidator : AbstractValidator<UpdateDatasourceCommand>
    {
        public UpdateDatasourceCommandValidator()
        {
            RuleFor(x => x.DatasourceRequest.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.DatasourceRequest.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.DatasourceRequest.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
