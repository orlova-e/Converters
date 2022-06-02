using AutoMapper;
using Converters.Domain.Entities;
using Converters.Web.Models.Convertations;

namespace Converters.Web.Services.Translations;

public class ConvertationToGetConvertationDtoMap : Profile
{
    public ConvertationToGetConvertationDtoMap()
    {
        CreateMap<Convertation, GetConvertationDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Created, o => o.MapFrom(x => x.Created))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.JsonDownloadPath, o => o.Ignore())
            .ForMember(x => x.XmlDownloadPath, o => o.Ignore());
    }
}