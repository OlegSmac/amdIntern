using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Users.Relations;
using Microsoft.AspNetCore.Identity;

namespace Vehicles.Domain.Users.Models;

public class User
{
    [Key]
    [ForeignKey("ApplicationUser")]
    public string Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    private string _firstName;

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }

    public ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
}