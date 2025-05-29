using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Vehicles.Responses;

public class VehicleDto
{
    public int Id { get; init; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string TransmissionType { get; set; }
    public required double EngineVolume { get; set; }
    public required int EnginePower { get; set; }
    public required string FuelType { get; set; }
    public required double FuelConsumption { get; set; }
    public required string Color { get; set; }
    public required int Mileage { get; set; }
    public required int MaxSpeed { get; set; }

    public static VehicleDto FromVehicle(Domain.VehicleTypes.Models.Vehicle vehicle)
    {
        return vehicle switch
        {
            Motorcycle motorcycle => MotorcycleDto.FromMotorcycle(motorcycle),
            Car car => CarDto.FromCar(car),
            Truck truck => TruckDto.FromTruck(truck),
            _ => throw new NotSupportedException("Unknown vehicle type")
        };
    }
}