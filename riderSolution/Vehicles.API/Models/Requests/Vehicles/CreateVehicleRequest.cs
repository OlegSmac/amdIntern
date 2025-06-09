namespace Vehicles.API.Models.Requests.Vehicles;

public class CreateVehicleRequest
{
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

    public string VehicleType { get; set; } // "car", "motorcycle", "truck"

    public CarInfo? CarInfo { get; set; }
    public MotorcycleInfo? MotorcycleInfo { get; set; }
    public TruckInfo? TruckInfo { get; set; }
}

public class CarInfo
{
    public string BodyType { get; set; }
    public int Seats { get; set; }
    public int Doors { get; set; }
}

public class MotorcycleInfo
{
    public bool HasSidecar { get; set; }
}

public class TruckInfo
{
    public string CabinType { get; set; }
    public int LoadCapacity { get; set; }
}
