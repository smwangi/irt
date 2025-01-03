using System.Data;
using System.Net;
using FluentValidation;

namespace Irt.Application.Datasources
{
    public class DatasourceDtoValidator : AbstractValidator<DatasourceDto>
    {
        public DatasourceDtoValidator()
        {
            RuleFor(x => x.Id).Empty()
            .WithMessage("Name should not be blank.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name should not be blank.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}