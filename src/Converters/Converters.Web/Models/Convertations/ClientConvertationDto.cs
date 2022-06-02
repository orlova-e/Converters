namespace Converters.Web.Models.Convertations;

public class ClientConvertationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Created { get; init; }
    public string JsonDownloadPath { get; init; }
    public string XmlDownloadPath { get; init; }
}