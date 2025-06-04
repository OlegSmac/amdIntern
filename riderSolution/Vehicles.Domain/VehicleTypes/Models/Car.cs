using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Car : Vehicle
{
    private string _bodyType;

    [MaxLength(40)]
    [Required]
    public string BodyType
    {
        get => _bodyType;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Body type is required.");
            if (value.Length > 50) throw new ArgumentException("Body type cannot exceed 50 characters.");
            
            _bodyType = value;
        }
    }

    private int _seats;

    [Required]
    public int Seats
    {
        get => _seats;
        set
        {
            if (value < 0) throw new ArgumentException("Seats cannot be less than zero.");
            
            _seats = value;
        }
    }

    private int _doors;

    [Required]
    public int Doors
    {
        get => _doors;
        set
        {
            if (value < 0) throw new ArgumentException("Doors cannot be less than zero.");
            
            _doors = value;
        }
    }
    
}