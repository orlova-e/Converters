using Microsoft.AspNetCore.SignalR;

namespace Converters.Web.Hubs;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.GetHttpContext().Session.Id;
    }
}