using Converters.Web.Models.Convertations;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class FileConvertedEvent : INotification
{
    public ClientConvertationDto Dto { get; }

    public FileConvertedEvent(ClientConvertationDto dto)
    {
        Dto = dto;
    }
}