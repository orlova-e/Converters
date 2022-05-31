using Converters.Web.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Converters.Web.Services.Commands.Convertations;

public class FileConvertedHandler : INotificationHandler<FileConvertedEvent>
{
    private readonly IHubContext<ConvertersHub> _hubContext;

    public FileConvertedHandler(IHubContext<ConvertersHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public Task Handle(FileConvertedEvent notification, CancellationToken cancellationToken)
    {
        return _hubContext.Clients.All.SendAsync("fileConverted", notification.Dto, cancellationToken);
    }
}