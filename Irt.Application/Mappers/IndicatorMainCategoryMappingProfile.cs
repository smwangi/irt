using AutoMapper;
using Irt.Application.IndicatorMainCategories;
using Irt.Core.IndicatorMainCategories;

namespace Irt.Application.Mappers;

public sealed class IndicatorMainCategoryMappingProfile : Profile
{
    public IndicatorMainCategoryMappingProfile()
    {
        CreateMap<IndicatorMainCategory, IndicatorMainCategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
