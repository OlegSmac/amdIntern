using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Abstractions;

public interface INotificationRepository : IRepository
{
    Task<List<Notification>> GetUnsentNotificationsAsync();

    Task SetNotificationReadAsync(int id);

    Task SetAdminNotificationResolvedAsync(int id);

}