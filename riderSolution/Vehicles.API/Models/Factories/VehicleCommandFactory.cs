using AutoMapper;
using MediatR;
using Vehicles.API.Contracts.Requests.Vehicles;
using Vehicles.Application.Requests.Vehicles.Cars.Commands;
using Vehicles.Application.Requests.Vehicles.Motorcycles.Commands;
using Vehicles.Application.Requests.Vehicles.Trucks.Commands;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.Factories;

public static class VehicleCommandFactory
{
    public static IRequest<Vehicle> CreateCommand(CreateVehicleRequest request, IMapper mapper)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleType)) throw new ArgumentException("VehicleType is required.");

        return request.VehicleType.ToLower() switch
        {
            "car" when request.CarInfo != null => new CreateCar(mapper.Map<Car>(request)),
            "motorcycle" when request.MotorcycleInfo != null => new CreateMotorcycle(mapper.Map<Motorcycle>(request)),
            "truck" when request.TruckInfo != null => new CreateTruck(mapper.Map<Truck>(request)),
            _ => throw new ArgumentException($"Invalid vehicle type or missing additional info: {request.VehicleType}")
        };
    }

    public static IRequest<Vehicle> UpdateCommand(UpdateVehicleRequest request, IMapper mapper)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleType)) throw new ArgumentException("VehicleType is required.");

        return request.VehicleType.ToLower() switch
        {
            "car" when request.CarInfo != null => new UpdateCar(mapper.Map<Car>(request)),
            "motorcycle" when request.MotorcycleInfo != null => new UpdateMotorcycle(mapper.Map<Motorcycle>(request)),
            "truck" when request.TruckInfo != null => new UpdateTruck(mapper.Map<Truck>(request)),
            _ => throw new ArgumentException($"Invalid vehicle type or missing additional info: {request.VehicleType}")
        };
    }
}