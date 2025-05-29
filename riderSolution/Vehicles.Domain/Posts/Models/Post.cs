using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Domain.Posts.Models;

public class Post
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public required string Body { get; set; }
    
    [Required]
    public required DateTime Date { get; set; }
    
    public required bool IsHidden { get; set; }
    
    public required int Views { get; set; }
    
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}