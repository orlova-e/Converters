namespace Converters.Web.Services.Interfaces;

public interface ITranslator
{
    TDestination Translate<TSource, TDestination>(TSource source);
}