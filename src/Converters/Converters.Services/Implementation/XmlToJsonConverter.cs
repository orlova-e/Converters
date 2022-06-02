using System.Text;
using System.Xml;
using Converters.Services.Interfaces;
using Newtonsoft.Json;

namespace Converters.Services.Implementation;

internal class XmlToJsonConverter : IConverter
{
    public byte[] Convert(Stream stream)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(stream);

        string json = JsonConvert.SerializeXmlNode(doc);
        return Encoding.UTF8.GetBytes(json);
    }
    
    public string Convert(string filePath, out byte[] data)
    {
        var text = File.ReadAllText(filePath);
        XmlDocument doc = new XmlDocument();
        doc.Load(text);
        
        string json = JsonConvert.SerializeXmlNode(doc);
        data = Encoding.UTF8.GetBytes(json);
        data = Encoding.UTF8.GetBytes(doc.OuterXml);

        return Path.ChangeExtension(filePath, "json");
    }
}