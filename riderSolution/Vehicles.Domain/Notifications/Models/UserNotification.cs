using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Domain.Notifications.Models;

public class UserNotification : Notification
{
    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
    
}