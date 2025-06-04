using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Truck : Vehicle
{
    private string _cabinType;

    [MaxLength(50)]
    [Required]
    public string CabinType
    {
        get => _cabinType;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Cabin type is required.");
            if (value.Length > 50) throw new ArgumentException("Cabin type cannot exceed 50 characters.");
            
            _cabinType = value;
        }
    }
    
    private int _loadCapacity;

    [Required]
    public int LoadCapacity
    {
        get => _loadCapacity;
        set
        {
            if (value < 0) throw new ArgumentException("Load capacity must be greater than or equal to 0.");
            
            _loadCapacity = value;
        }
    }
}