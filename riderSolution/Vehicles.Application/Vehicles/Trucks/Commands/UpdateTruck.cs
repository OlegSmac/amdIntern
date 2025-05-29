using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record UpdateTruck(int Id, string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    string CabinType, int LoadCapacity, int TotalWeight) : IRequest<TruckDto>;

public class UpdateTruckHandler : IRequestHandler<UpdateTruck, TruckDto>
{
    public IUnitOfWork _unitOfWork;

    public UpdateTruckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TruckDto> Handle(UpdateTruck request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Id} exists.");
        
        Truck truck = (Truck)vehicle;
        truck.Brand = request.Brand;
        truck.Model = request.Model;
        truck.Year = request.Year;
        truck.MaxSpeed = request.MaxSpeed;
        truck.TransmissionType = request.TransmissionType;
        truck.EnginePower = request.EnginePower;
        truck.EngineVolume = request.EngineVolume;
        truck.FuelType = request.FuelType;
        truck.FuelConsumption = request.FuelConsumption;
        truck.Color = request.Color;
        truck.Mileage = request.Mileage;
        truck.CabinType = request.CabinType;
        truck.LoadCapacity = request.LoadCapacity;
        truck.TotalWeight = request.TotalWeight;
        
        await _unitOfWork.VehicleRepository.UpdateAsync(truck);
        await _unitOfWork.SaveAsync();

        return TruckDto.FromTruck(truck);
    }
}