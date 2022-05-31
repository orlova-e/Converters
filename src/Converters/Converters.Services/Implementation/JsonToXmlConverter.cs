using System.Text;
using System.Xml;
using Converters.Services.Interfaces;
using Newtonsoft.Json;

namespace Converters.Services.Implementation;

internal class JsonToXmlConverter : IConverter
{
    public byte[] Convert(Stream stream)
    {
        string json = Encoding.UTF8.GetString((stream as MemoryStream).ToArray());
        
        XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
        return Encoding.UTF8.GetBytes(doc.OuterXml);
    }

    public string Convert(FileStream fileStream, out byte[] data)
    {
        using MemoryStream memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        
        string json = Encoding.UTF8.GetString(memoryStream.ToArray());
        XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
        data = Encoding.UTF8.GetBytes(doc.OuterXml);

        return Path.ChangeExtension(fileStream.Name, "xml");
    }
}