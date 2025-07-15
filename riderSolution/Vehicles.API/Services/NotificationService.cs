using Microsoft.AspNetCore.SignalR;
using Vehicles.API.Hubs;
using Vehicles.Application.Abstractions;

namespace Vehicles.API.Services;

public class NotificationService : INotificationSender
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task SendUnreadCountAsync(string userId, int unreadCount)
    {
        await _hubContext.Clients
            .Group(userId)
            .SendAsync("UpdateUnreadCount", unreadCount);
    }

}