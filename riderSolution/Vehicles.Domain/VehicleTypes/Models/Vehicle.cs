using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public abstract class Vehicle : BaseEntity
{
    private string _brand;

    [MaxLength(50)]
    [Required]
    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Brand is required.");
            if (value.Length > 50) throw new ArgumentException("Brand cannot exceed 50 characters.");

            _brand = value;
        }
    }

    private string _model;

    [MaxLength(50)]
    [Required]
    public string Model
    {
        get => _model;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Model is required.");
            if (value.Length > 50) throw new ArgumentException("Model cannot exceed 50 characters.");

            _model = value;
        }
    }
    
    private string _transmission;

    [MaxLength(50)]
    [Required]
    public string TransmissionType
    {
        get => _transmission;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Transmission type is required.");
            if (value.Length > 50) throw new ArgumentException("Transmission type cannot exceed 50 characters.");

            _transmission = value;
        }
    }

    private string _fuelType;

    [MaxLength(50)]
    [Required]
    public string FuelType
    {
        get => _fuelType;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Fuel type is required.");
            if (value.Length > 50) throw new ArgumentException("Fuel type cannot exceed 50 characters.");

            _fuelType = value;
        }
    }
    
    private string _color;

    [MaxLength(30)]
    [Required]
    public string Color
    {
        get => _color;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Color is required.");
            if (value.Length > 50) throw new ArgumentException("Color cannot exceed 30 characters.");

            _color = value;
        }
    }

    private int _year;

    [Required]
    public int Year
    {
        get => _year;
        set
        {
            if (value > DateTime.Now.Year) throw new ArgumentException("Year cannot exceed DateTime.Now year.");
            
            _year = value;
        }
    }

    private int _enginePower;

    [Required]
    public int EnginePower
    {
        get => _enginePower;
        set
        {
            if (value < 0) throw new ArgumentException("Engine power cannot be negative.");
            
            _enginePower = value;
        }
    }
    
    private int _mileage;

    [Required]
    public int Mileage
    {
        get => _mileage;
        set
        {
            if (value < 0) throw new ArgumentException("Mileage cannot be negative.");
            
            _mileage = value;
        }
    }

    private int _maxSpeed;

    [Required]
    public int MaxSpeed
    {
        get => _maxSpeed;
        set
        {
            if (value < 0) throw new ArgumentException("MaxSpeed cannot be negative.");
            
            _maxSpeed = value;
        }
    }

    private double _engineVolume;

    [Required]
    public double EngineVolume
    {
        get => _engineVolume;
        set
        {
            if (value < 0) throw new ArgumentException("Engine volume cannot be negative.");
            
            _engineVolume = value;
        }
    }

    private double _fuelConsumption;

    [Required]
    public double FuelConsumption
    {
        get => _fuelConsumption;
        set
        {
            if (value < 0) throw new ArgumentException("Fuel consumption cannot be negative.");
            
            _fuelConsumption = value;
        }
    }
    
}