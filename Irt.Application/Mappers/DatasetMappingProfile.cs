using Irt.Core.ValueObjects;

namespace Irt.Application.Mappers;

using AutoMapper;
using Irt.Application.Datasets;
using Irt.Core.Datasets;

public class DatasetMappingProfile : Profile
{
    public DatasetMappingProfile()
    {
        CreateMap<Dataset, DatasetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DatasourceId, opt => opt.MapFrom(src => src.Datasource.Id.Value))
            .ForMember(dest => dest.DatasetType, opt => opt.MapFrom(src => src.DatasetType.Value))
            .ForMember(dest => dest.IndicatorDefinitionId, opt => opt.MapFrom(src => src.IndicatorDefinition.Id.Value));
        CreateMap<DatasetType, string>()
            .ConstructUsing(dto => dto.Value);
        CreateMap<string, DatasetType>()
            .ConstructUsing(value => DatasetType.Parse(value));
    }
}
