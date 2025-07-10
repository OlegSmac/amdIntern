using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Domain.Notifications.Models;

public class CompanyNotification : Notification
{
    [Required]
    public string CompanyId { get; set; }
    public Company Company { get; set; }
}