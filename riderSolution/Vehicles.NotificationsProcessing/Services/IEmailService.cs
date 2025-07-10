using Vehicles.Domain.Notifications.Models;

namespace Vehicles.NotificationsProcessing.Services;

public interface IEmailService
{
    Task SendAsync(Notification notification);
}