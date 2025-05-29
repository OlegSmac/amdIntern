using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Cars.Responses;

public class CarDto : VehicleDto
{
    public required string BodyType { get; set; }
    public required int Seats { get; set; }
    public required int Doors { get; set; } 

    public static CarDto FromCar(Car car)
    {
        return new CarDto()
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            TransmissionType = car.TransmissionType,
            EngineVolume = car.EngineVolume,
            EnginePower = car.EnginePower,
            FuelType = car.FuelType,
            FuelConsumption = car.FuelConsumption,
            Color = car.Color,
            Mileage = car.Mileage,
            MaxSpeed = car.MaxSpeed,
            BodyType = car.BodyType,
            Seats = car.Seats,
            Doors = car.Doors
        };
    }
}