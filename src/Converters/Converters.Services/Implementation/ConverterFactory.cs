using Converters.Services.Interfaces;

namespace Converters.Services.Implementation;

public static class ConverterFactory
{
    public static IConverter GetConverter(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        
        return extension switch
        {
            ".xml" => new XmlToJsonConverter(),
            ".json" => new JsonToXmlConverter(),
            { } => throw new ArgumentException($"No converters for extension '{extension}' can be provided")
        };
    }
}