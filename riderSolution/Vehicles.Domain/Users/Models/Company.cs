using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Domain.Users.Models;

public class Company
{
    public int Id { get; init; }
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Description { get; set; }
    public ICollection<CompanyNotification> CompanyNotifications { get; set; } = new List<CompanyNotification>();
    public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
}