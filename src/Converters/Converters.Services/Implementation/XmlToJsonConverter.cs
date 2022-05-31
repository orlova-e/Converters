using System.Text;
using System.Xml;
using Converters.Services.Interfaces;
using Newtonsoft.Json;

namespace Converters.Services.Implementation;

internal class XmlToJsonConverter : IConverter
{
    public byte[] Convert(Stream stream)
    {
    //    string xml = Encoding.UTF8.GetString((stream as MemoryStream).ToArray());
        XmlDocument doc = new XmlDocument();
        doc.Load(stream);
       // doc.LoadXml(xml);

        string json = JsonConvert.SerializeXmlNode(doc);
        return Encoding.UTF8.GetBytes(json);
    }

    public string Convert(FileStream fileStream, out byte[] data)
    {
        // using MemoryStream memoryStream = new MemoryStream();
        // fileStream.CopyTo(memoryStream);
        
        XmlDocument doc = new XmlDocument();
        doc.Load(fileStream);
        
        string json = JsonConvert.SerializeXmlNode(doc);
        data = Encoding.UTF8.GetBytes(json);

        return Path.ChangeExtension(fileStream.Name, "json");
    }
}