namespace Vehicles.API.Models.DTOs.Notifications;

public class AdminNotificationDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSent { get; set; }
    public bool IsRead { get; set; }
    public string AdminId { get; set; }
    public bool IsResolved { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}