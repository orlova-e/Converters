using AutoMapper;
using Converters.Web.Services.Interfaces;

namespace Converters.Web.Services.Implementation;

public class Translator : ITranslator
{
    private readonly IMapper _mapper;

    public Translator(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public TDestination Translate<TSource, TDestination>(TSource source)
        => _mapper.Map<TSource, TDestination>(source);
}