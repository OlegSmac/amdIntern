using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Trucks.Responses;

public class TruckDto : VehicleDto
{
    public required string CabinType { get; set; }
    public required int LoadCapacity { get; set; }
    public required int TotalWeight { get; set; }

    public static TruckDto FromTruck(Truck truck)
    {
        return new TruckDto()
        {
            Id = truck.Id,
            Brand = truck.Brand,
            Model = truck.Model,
            Year = truck.Year,
            TransmissionType = truck.TransmissionType,
            EngineVolume = truck.EngineVolume,
            EnginePower = truck.EnginePower,
            FuelType = truck.FuelType,
            FuelConsumption = truck.FuelConsumption,
            Color = truck.Color,
            Mileage = truck.Mileage,
            MaxSpeed = truck.MaxSpeed,
            CabinType = truck.CabinType,
            LoadCapacity = truck.LoadCapacity,
            TotalWeight = truck.TotalWeight
        };
    }
}