using Converters.Web.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Converters.Web.Services.Commands.Convertations;

public class FileConvertedHandler : INotificationHandler<FileConvertedEvent>
{
    private readonly IHubContext<ConvertersHub> _hubContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileConvertedHandler(
        IHubContext<ConvertersHub> hubContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _hubContext = hubContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Task Handle(FileConvertedEvent notification, CancellationToken cancellationToken)
    {
        var sessionId = _httpContextAccessor.HttpContext.Session.Id;
        return _hubContext.Clients.Client(sessionId).SendAsync("fileConverted", notification.Dto, cancellationToken);
    }
}