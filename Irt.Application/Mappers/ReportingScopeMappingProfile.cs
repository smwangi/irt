using AutoMapper;
using Irt.Application.ReportingScopes;
using Irt.Core.ReportingScopes;

namespace Irt.Application.Mappers;

public class ReportingScopeMappingProfile : Profile
{
    public ReportingScopeMappingProfile()
    {
        CreateMap<ReportingScope, ReportingScopeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
