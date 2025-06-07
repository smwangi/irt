using AutoMapper;
using Irt.Application.ReportingScopes;
using Irt.Core.ReportingScopes;

namespace Irt.Application.Mappers;

public class ReportingScopeMappingProfile : Profile
{
    public ReportingScopeMappingProfile()
    {
        CreateMap<ReportingScope, ReportingScopeDto>().ReverseMap();
    }
}