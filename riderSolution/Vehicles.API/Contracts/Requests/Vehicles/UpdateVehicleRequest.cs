namespace Vehicles.API.Contracts.Requests.Vehicles;

public class UpdateVehicleRequest
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string TransmissionType { get; set; }
    public string FuelType { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public int EnginePower { get; set; }
    public int Mileage { get; set; }
    public int MaxSpeed { get; set; }
    public double EngineVolume { get; set; }
    public double FuelConsumption { get; set; }

    public string VehicleType { get; set; }

    public CarInfo? CarInfo { get; set; }
    public MotorcycleInfo? MotorcycleInfo { get; set; }
    public TruckInfo? TruckInfo { get; set; }
}