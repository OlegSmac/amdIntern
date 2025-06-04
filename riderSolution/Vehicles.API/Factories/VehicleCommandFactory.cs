using MediatR;
using Vehicles.API.Requests;
using Vehicles.Application.Vehicles.Cars.Commands;
using Vehicles.Application.Vehicles.Motorcycles.Commands;
using Vehicles.Application.Vehicles.Trucks.Commands;
using Vehicles.Application.Vehicles.Vehicles.Commands;
using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Factories;

public static class VehicleCommandFactory
{
    private static void SetCommonFields(Vehicle vehicle, VehicleRequest request)
    {
        vehicle.Brand = request.Brand;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.MaxSpeed = request.MaxSpeed;
        vehicle.TransmissionType = request.TransmissionType;
        vehicle.EngineVolume = request.EngineVolume;
        vehicle.EnginePower = request.EnginePower;
        vehicle.FuelType = request.FuelType;
        vehicle.FuelConsumption = request.FuelConsumption;
        vehicle.Color = request.Color;
        vehicle.Mileage = request.Mileage;
    }
    
    private static Car CreateCarFromRequest(VehicleRequest request)
    {
        var car = new Car
        {
            BodyType = request.CarInfo.BodyType,
            Seats = request.CarInfo.Seats,
            Doors = request.CarInfo.Doors
        };
        SetCommonFields(car, request);
        
        return car;
    }

    private static Motorcycle CreateMotorcycleFromRequest(VehicleRequest request)
    {
        var motorcycle = new Motorcycle
        {
            HasSidecar = request.MotorcycleInfo.HasSidecar
        };
        SetCommonFields(motorcycle, request);

        return motorcycle;
    }

    private static Truck CreateTruckFromRequest(VehicleRequest request)
    {
        var truck = new Truck
        {
            CabinType = request.TruckInfo.CabinType,
            LoadCapacity = request.TruckInfo.LoadCapacity
        };
        SetCommonFields(truck, request);
        
        return truck;
    }
    
    public static IRequest<VehicleDto> CreateCommand(VehicleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleType))
            throw new ArgumentException("VehicleType is required.");

        return request.VehicleType.ToLower() switch
        {
            "car" when request.CarInfo != null => new CreateCar(CreateCarFromRequest(request)),
            "motorcycle" when request.MotorcycleInfo != null => new CreateMotorcycle(CreateMotorcycleFromRequest(request)),
            "truck" when request.TruckInfo != null => new CreateTruck(CreateTruckFromRequest(request)),
            _ => throw new ArgumentException($"Invalid vehicle type or missing additional info: {request.VehicleType}")
        };
    }

    public static IRequest<VehicleDto> UpdateCommand(VehicleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleType))
            throw new ArgumentException("VehicleType is required.");

        return request.VehicleType.ToLower() switch
        {
            "car" when request.CarInfo != null => new UpdateCar(CreateCarFromRequest(request)),
            "motorcycle" when request.MotorcycleInfo != null => new UpdateMotorcycle(CreateMotorcycleFromRequest(request)),
            "truck" when request.TruckInfo != null => new UpdateTruck(CreateTruckFromRequest(request)),
            _ => throw new ArgumentException($"Invalid vehicle type or missing additional info: {request.VehicleType}")
        };
    }
}
