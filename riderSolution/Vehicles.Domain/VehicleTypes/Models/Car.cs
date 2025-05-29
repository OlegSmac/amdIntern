using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Car : Vehicle
{
    [MaxLength(40)]
    [Required]
    public required string BodyType { get; set; }
    
    [Required]
    public required int Seats { get; set; }
    
    [Required]
    public required int Doors { get; set; }
    
    public void UpdateFrom(
        string brand, string model, int year, int maxSpeed, string transmissionType,
        double engineVolume, int enginePower, string fuelType, double fuelConsumption,
        string color, int mileage, string bodyType, int seats, int doors)
    {
        Brand = brand;
        Model = model;
        Year = year;
        MaxSpeed = maxSpeed;
        TransmissionType = transmissionType;
        EngineVolume = engineVolume;
        EnginePower = enginePower;
        FuelType = fuelType;
        FuelConsumption = fuelConsumption;
        Color = color;
        Mileage = mileage;
        BodyType = bodyType;
        Seats = seats;
        Doors = doors;
    }

}