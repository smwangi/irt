using AutoMapper;
using Irt.Application.IndicatorDefinitions;
using Irt.Core.IndicatorDefinitions;

namespace Irt.Application.Mappers;

public class IndicatorDefinitionMappingProfile : Profile
{
    public IndicatorDefinitionMappingProfile()
    {
        CreateMap<IndicatorDefinition, IndicatorDefinitionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReportingScopeId, opt => opt.MapFrom(src => src.ReportingScope.Id.Value))
            .ForMember(dest => dest.ReportingScopeName, opt => opt.MapFrom(src => src.ReportingScope.Name.Value))
            .ForMember(dest => dest.UnitOfMeasureId, opt => opt.MapFrom(src => src.UnitOfMeasure.Id.Value))
            .ForMember(dest => dest.UnitOfMeasureName, opt => opt.MapFrom(src => src.UnitOfMeasure.Name.Value))
            .ForMember(dest => dest.IndicatorCategoryId, opt => opt.MapFrom(src => src.IndicatorCategory.Id.Value))
            .ForMember(dest => dest.IndicatorCategoryName, opt => opt.MapFrom(src => src.IndicatorCategory.Name.Value))
            .ForMember(dest => dest.MinThreshold, opt => opt.MapFrom(src => src.MinThreshold))
            .ForMember(dest => dest.MaxThreshold, opt => opt.MapFrom(src => src.MaxThreshold))
            .ForMember(dest => dest.Formula, opt => opt.MapFrom(src => src.Formula))
            .ForMember(dest => dest.FormulaDescription, opt => opt.MapFrom(src => src.FormulaDescription))
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src.Metadata))
            .ForMember(dest => dest.DPSIR, opt => opt.MapFrom(src => src.DPSIR));
    }
}
