using AutoMapper;
using Irt.Application.Datasources;
using Irt.Core.Datasources;
using Irt.Core.ValueObjects;

namespace Irt.Application.Mappers;

public class DatasourceMappingProfile : Profile
{
    public DatasourceMappingProfile()
    {
        CreateMap<Name, string>().ConvertUsing(name => name.Value);
        CreateMap<Datasource, DatasourceDto>()
            .ForMember(dest =>
                dest.Description, opt =>
                opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Description) ? "N/A" : src.Description))
            .ForMember(dest =>
                dest.Name, opt =>
                    opt.MapFrom(src =>
                        src.Name.Value))
            .ForMember(dest =>
                dest.Id, opt =>
                    opt.MapFrom(src =>
                        src.Id.Value))
            .ForMember(dest =>
                dest.Source, opt =>
                    opt.MapFrom(src => src.Source))
            .ForMember(dest =>
                dest.DatasourceType, opt =>
                opt.MapFrom(src =>
                    ParseDatasourceType(src.DatasourceType.ToString())));
    }
    
    private static Core.Datasources.DatasourceType ParseDatasourceType(string datasourceType)
    {
        return Enum.TryParse<Core.Datasources.DatasourceType>(datasourceType, out var result)
            ? result
            : Core.Datasources.DatasourceType.File;
    }
}