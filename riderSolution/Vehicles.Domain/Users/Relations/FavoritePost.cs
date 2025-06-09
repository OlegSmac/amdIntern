using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Domain.Users.Relations;

public class FavoritePost
{
    [Required]
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}