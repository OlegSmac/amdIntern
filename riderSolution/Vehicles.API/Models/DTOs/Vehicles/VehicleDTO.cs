namespace Vehicles.API.Models.DTOs.Vehicles;

public class VehicleDTO
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string TransmissionType { get; set; } = string.Empty;
    public double EngineVolume { get; set; }
    public int EnginePower { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public double FuelConsumption { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public int MaxSpeed { get; set; }
}