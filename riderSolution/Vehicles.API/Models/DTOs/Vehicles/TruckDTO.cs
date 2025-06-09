namespace Vehicles.API.Models.DTOs.Vehicles;

public class TruckDTO : VehicleDTO
{
    public string CabinType { get; set; } = string.Empty;
    public int LoadCapacity { get; set; }
}