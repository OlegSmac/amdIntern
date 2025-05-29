using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record CreateTruck(string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    string CabinType, int LoadCapacity, int TotalWeight) : IRequest<TruckDto>;

public class CreateTruckHandler : IRequestHandler<CreateTruck, TruckDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTruckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TruckDto> Handle(CreateTruck request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var truck = new Truck()
        {
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year,
            TransmissionType = request.TransmissionType,
            EngineVolume = request.EngineVolume,
            EnginePower = request.EnginePower,
            FuelType = request.FuelType,
            FuelConsumption = request.FuelConsumption,
            Color = request.Color,
            Mileage = request.Mileage,
            MaxSpeed = request.MaxSpeed,
            CabinType = request.CabinType,
            LoadCapacity = request.LoadCapacity,
            TotalWeight = request.TotalWeight
        };

        await _unitOfWork.VehicleRepository.CreateAsync(truck);
        await _unitOfWork.SaveAsync();
        await _unitOfWork.CommitTransactionAsync();

        return TruckDto.FromTruck(truck);
    }
}