namespace Vehicles.API.Models.DTOs.Vehicles;

public class CarDTO : VehicleDTO
{
    public string BodyType { get; set; } = string.Empty;
    public int Seats { get; set; }
    public int Doors { get; set; }
}