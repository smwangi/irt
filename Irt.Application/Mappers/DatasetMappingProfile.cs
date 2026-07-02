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
            //.ForMember(dest => dest.DatasetType, opt => opt.MapFrom(src => src.DatasetType.Value))
            .ReverseMap();
        CreateMap<DatasetType, string>()
            .ConstructUsing(dto => dto.Value);
        CreateMap<string, DatasetType>()
            .ConstructUsing(value => DatasetType.Parse(value));
    }
}