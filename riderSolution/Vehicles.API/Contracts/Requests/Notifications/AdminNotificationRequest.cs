namespace Vehicles.API.Contracts.Requests.Notifications;

public class AdminNotificationRequest
{
    public string Company { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}