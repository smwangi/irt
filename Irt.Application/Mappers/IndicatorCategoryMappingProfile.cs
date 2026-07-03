using AutoMapper;
using Irt.Application.IndicatorCategories;
using Irt.Core.IndicatorCategories;

namespace Irt.Application.Mappers;

public sealed class IndicatorCategoryMappingProfile : Profile
{
    public IndicatorCategoryMappingProfile()
    {
        CreateMap<IndicatorCategory, IndicatorCategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.IndicatorMainCategoryId, opt => opt.MapFrom(src => src.IndicatorMainCategory.Id.Value))
            .ForMember(dest => dest.IndicatorMainCategoryName, opt => opt.MapFrom(src => src.IndicatorMainCategory.Name.Value));
    }
}
