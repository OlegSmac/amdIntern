using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.Notifications.Models;

public class Notification
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Title { get; set; }
    
    [Required]
    public string Body { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}