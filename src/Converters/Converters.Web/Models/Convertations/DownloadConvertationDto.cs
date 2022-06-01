using Converters.Domain.Conditions;

namespace Converters.Web.Models.Convertations;

public class DownloadConvertationDto
{
    public Guid Id { get; init; }
    public FileType Type { get; init; }
}