using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Motorcycles.Responses;

public class MotorcycleDto : VehicleDto
{
    public bool HasSidecar { get; set; }

    public static MotorcycleDto FromMotorcycle(Motorcycle motorcycle)
    {
        return new MotorcycleDto()
        {
            Id = motorcycle.Id,
            Brand = motorcycle.Brand,
            Model = motorcycle.Model,
            Year = motorcycle.Year,
            TransmissionType = motorcycle.TransmissionType,
            EngineVolume = motorcycle.EngineVolume,
            EnginePower = motorcycle.EnginePower,
            FuelType = motorcycle.FuelType,
            FuelConsumption = motorcycle.FuelConsumption,
            Color = motorcycle.Color,
            Mileage = motorcycle.Mileage,
            MaxSpeed = motorcycle.MaxSpeed,
            HasSidecar = motorcycle.HasSidecar
        };
    }
}