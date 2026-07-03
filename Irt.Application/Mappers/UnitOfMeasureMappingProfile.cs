using AutoMapper;
using Irt.Application.UnitOfMeasurements;
using Irt.Core.UnitOfMeasurements;

namespace Irt.Application.Mappers;

public sealed class UnitOfMeasureMappingProfile : Profile
{
    public UnitOfMeasureMappingProfile()
    {
        CreateMap<UnitOfMeasure, UnitOfMeasureDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
