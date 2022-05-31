using Converters.Web.Models.Convertations;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class FileConvertedEvent : INotification
{
    public GetConvertationDto Dto { get; }

    public FileConvertedEvent(GetConvertationDto dto)
    {
        Dto = dto;
    }
}