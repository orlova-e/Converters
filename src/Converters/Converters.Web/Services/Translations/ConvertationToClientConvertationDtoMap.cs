using AutoMapper;
using Converters.Domain.Conditions;
using Converters.Domain.Entities;
using Converters.Web.Controllers;
using Converters.Web.Models.Convertations;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Converters.Web.Services.Translations;

public class ConvertationToClientConvertationDtoMap : Profile
{
    public ConvertationToClientConvertationDtoMap()
    {
        CreateMap<Convertation, ClientConvertationDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Created, o => o.MapFrom(x => x.Created.ToString("g")))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.JsonDownloadPath, o => o.MapFrom(x => GetLink(x.Id, FileType.Json)))
            .ForMember(x => x.XmlDownloadPath, o => o.MapFrom(x => GetLink(x.Id, FileType.Xml)));
    }
    
    private string GetLink(Guid id, FileType type)
    {
        return $"/download/?id={id}&type={type}";
    }
}