using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models.VehicleModels;

public class Model
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
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Model is required.");
            if (value.Length > 50) throw new ArgumentException("Model cannot exceed 50 characters.");
            
            _name = value;
        }
    }
    
    public virtual Brand Brand { get; set; }
    
    public virtual ICollection<Year> Years { get; set; } = new List<Year>();
}