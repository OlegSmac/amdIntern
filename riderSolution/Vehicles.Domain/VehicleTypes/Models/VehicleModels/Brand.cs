using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models.VehicleModels;

public class Brand
{
    public int Id { get; set; }

    private string _name;

    [MaxLength(50)]
    [Required]
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty.");
            if (value.Length > 50) throw new ArgumentException("Name cannot be longer than 50 characters.");
            
            _name = value;
        }
    }
    
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}