using FluentValidation;
using Irt.Application.Common;
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
        string DatasourceType) : ICommand<Result<DatasourceDto>>, IRequireMetadata
    {
        public string? UserId { get; private set; }
        public string? UserName { get; private set; }
        public string? Application { get; private set; }
        public string? IpAddress { get; private set; }
        public void SetMetadata(string userId, string userName, string application, string ipAddress)
        {
            UserId = userId;
            UserName = userName;
            Application = application;
            IpAddress = ipAddress;
        }
    }

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
