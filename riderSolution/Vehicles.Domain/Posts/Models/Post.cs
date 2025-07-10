using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Domain.Posts.Models;

public class Post : BaseEntity
{
    private string _title;

    [MaxLength(100)]
    [Required]
    public string Title
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
    public string Body
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
    public DateTime Date
    {
        get => _date;
        set
        {
            if (value > DateTime.Now) throw new ArgumentException("Date cannot be in the future.");
            
            _date = value;
        }
    }
    
    public bool IsHidden { get; set; }
    
    private int _views;

    public int Views
    {
        get => _views;
        set
        {
            if (value < 0) throw new ArgumentException("Views cannot be negative.");
            
            _views = value;
        }
    }

    private int _price;

    public int Price
    {
        get => _price;
        set
        {
            if (value < 0) throw new ArgumentException("Price cannot be negative.");
            
            _price = value;
        }
    }

    public List<PostImage> Images { get; set; } = new();
    
    public string CompanyId { get; set; }
    public Company Company { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}