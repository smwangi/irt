namespace Irt.Application.Mappers;

using AutoMapper;
using Irt.Application.Datasets;
using Irt.Core.Datasets;

public class DatasetMappingProfile : Profile
{
    public DatasetMappingProfile()
    {
        CreateMap<Dataset, DatasetDto>()
            .ForMember(dest =>
                dest.Description, opt =>
                opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Description) ? "N/A" : src.Description))
            .ForMember(dest =>
                dest.DatasetType, opt =>
                opt.MapFrom(src =>
                    ParseDatasetType(src.DatasetType.ToString())));
        /*.ForMember(dest =>
            dest.CreatedAt, opt =>
                opt.MapFrom(src =>
                    src.CreatedAt))
        .ForMember(dest =>
            dest.LastModifiedAt, opt =>
                opt.MapFrom(src => src.LastModifiedAt))
        .ForMember(dest =>
            dest.Name, opt =>
                opt.MapFrom(src =>
                    src.Name));*/



    }

    private static Core.Datasets.DatasetType ParseDatasetType(string datasetType)
    {
        return Enum.TryParse<Core.Datasets.DatasetType>(datasetType, out var result)
            ? result
            : Core.Datasets.DatasetType.Internal;
    }

}