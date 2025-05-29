using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Domain.Users.Models;

public class RegularUser
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

    public ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
}