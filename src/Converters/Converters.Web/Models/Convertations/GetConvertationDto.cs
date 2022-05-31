namespace Converters.Web.Models.Convertations;

public class GetConvertationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime Created { get; init; }
    public string JsonDownloadPath { get; init; }
    public string XmlDownloadPath { get; init; }
}