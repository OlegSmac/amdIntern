namespace Vehicles.Application.Abstractions;

public interface INotificationSender
{
    Task SendUnreadCountAsync(string userId, int unreadCount);
}