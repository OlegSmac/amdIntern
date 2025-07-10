namespace Vehicles.API.Models.DTOs.Notifications;

public class CompanyNotificationDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSent { get; set; }
    public bool IsRead { get; set; }
    public string CompanyId { get; set; }
}