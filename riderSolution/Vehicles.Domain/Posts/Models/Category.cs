using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicles.Domain.Posts.Models;

public class Category
{
    public int Id { get; set; }

    private string _name;

    [MaxLength(40)]
    [Required]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
        }
    }
    
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}