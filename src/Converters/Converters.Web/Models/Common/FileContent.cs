namespace Converters.Web.Models.Common;

public class FileContent
{
    public const string DefaultContentType = "application/octet-stream";

    public string FileName { get; }
    public Stream Content { get; }
    public string ContentType { get; }

    public FileContent(string filename, string contentType, Stream content)
    {
        FileName = filename;
        ContentType = contentType;
        Content = content;
    }
}