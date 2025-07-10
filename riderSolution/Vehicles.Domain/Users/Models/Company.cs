using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Domain.Users.Models;

public class Company
{
    [Key]
    [ForeignKey("ApplicationUser")]
    public string Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; } 

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
    
    public ICollection<CompanyNotification> CompanyNotifications { get; set; } = new List<CompanyNotification>();
    public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
}