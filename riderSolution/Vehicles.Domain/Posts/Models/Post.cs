using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Domain.Posts.Models;

public class Post
{
    public int Id { get; set; }

    private string _title;

    [MaxLength(100)]
    [Required]
    public required string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title type is required.");
            if (value.Length > 100) throw new ArgumentException("Title type cannot exceed 100 characters.");
            
            _title = value;
        }
    }

    private string _body;

    [Required]
    public required string Body
    {
        get => _body;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Body type is required.");
            
            _body = value;
        }
    }

    private DateTime _date;

    [Required]
    public required DateTime Date
    {
        get => _date;
        set
        {
            if (value > DateTime.Now) throw new ArgumentException("Date cannot be in the future.");
            
            _date = value;
        }
    }
    
    public required bool IsHidden { get; set; }


    private int _views;

    public required int Views
    {
        get => _views;
        set
        {
            if (value < 0) throw new ArgumentException("Views cannot be negative.");
            
            _views = value;
        }
    }
    
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}