using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.DTOs.Vehicles;

public static class VehicleType
{
    public static Type GetDtoType(Vehicle vehicle)
    {
        return vehicle switch
        {
            Car => typeof(CarDTO),
            Motorcycle => typeof(MotorcycleDTO),
            Truck => typeof(TruckDTO),
            _ => typeof(VehicleDTO)
        };
    }
}