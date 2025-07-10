using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Domain.Users.Models;

public class Admin
{
    [Key]
    [ForeignKey("ApplicationUser")]
    public string Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; } 

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }
    
    public ICollection<AdminNotification> AdminNotifications { get; set; } = new List<AdminNotification>();
}