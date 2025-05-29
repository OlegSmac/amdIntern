using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.Posts.Models;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(40)]
    [Required]
    public string Name { get; set; }
    
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}